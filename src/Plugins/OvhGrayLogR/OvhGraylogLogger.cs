using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core;
using Serilog.Sinks.Graylog.Core.MessageBuilders;
using System.Net;

namespace OvhGrayLogR;

public class OvhGraylogLogger
{
    private readonly Serilog.Core.Logger _logger;
    private readonly LogRPush.Category _minimumLevel;
    private readonly GrayLoggerConfiguration _settings;

    public OvhGraylogLogger(GrayLoggerConfiguration config)
    {
        _settings = config;
        _logger = CreateLogger();
        _minimumLevel = config.MinimumLogEventLevel;
    }

	public Serilog.Core.Logger CreateLogger()
	{
        var options = new GraylogSinkOptions
        {
            HostnameOrAddress = _settings.Host,
            Port = _settings.Port,
            TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Udp,
            MinimumLogEventLevel = Serilog.Events.LogEventLevel.Verbose
        };

        var builders = new Dictionary<BuilderType, Lazy<IMessageBuilder>>
        {
            [BuilderType.Exception] = new Lazy<IMessageBuilder>(() =>
            {
                string hostName = Dns.GetHostName();
                return new ExceptionMessageBuilder(hostName, options);
            }),
            [BuilderType.Message] = new Lazy<IMessageBuilder>(() =>
            {
                string hostName = Dns.GetHostName();
                return new CustomGelfMessageBuilder(hostName, options, _settings.OvhKey);
            })
        };

        options.GelfConverter = new GelfConverter(builders);

        var result = new LoggerConfiguration().WriteTo.Graylog(options).CreateLogger();
        return result;
    }

	public void SendLog(LogRPush.LogInfo logInfo)
	{
        if (logInfo.Category < _minimumLevel)
		{
            return;
		}

        var templateMessage = "{message} , {@" + _settings.PrefixName + "}";
		var templateException = "{message} {exceptionstack}, {@" + _settings.PrefixName + "}";

		switch (logInfo.Category)
		{
            case LogRPush.Category.Trace:
                _logger.Verbose(templateMessage, logInfo.Message, CreateLogParameter(logInfo));
                break;
            case LogRPush.Category.Sql:
            case LogRPush.Category.Debug:
			    _logger.Debug(templateMessage, logInfo.Message, CreateLogParameter(logInfo));
                break;
            case LogRPush.Category.Info:
                _logger.Information(templateMessage, logInfo.Message, CreateLogParameter(logInfo));
                break;
            case LogRPush.Category.Notification:
                _logger.Information(templateMessage, logInfo.Message, CreateLogParameter(logInfo));
				// TODO : Send notification by email
				break;
			case LogRPush.Category.Warn:
                _logger.Warning(templateMessage, logInfo.Message, CreateLogParameter(logInfo));
                break;
			case LogRPush.Category.Error:
                _logger.Error(templateException, logInfo.Message, logInfo.ExceptionStack, CreateLogParameter(logInfo));
                break;
			case LogRPush.Category.Fatal:
                _logger.Fatal(templateException, logInfo.Message, logInfo.ExceptionStack, CreateLogParameter(logInfo));
                break;
			default:
				break;
		}
	}

    public Dictionary<string, object> CreateLogParameter(LogRPush.LogInfo logParam)
    {
		var result = new Dictionary<string, object>();
        result.Add("Application", logParam.ApplicationName);
        result.Add("CurrentDate", DateTime.Now);
        result.Add("MachineName", logParam.MachineName);
        result.Add("EnvironmentName", logParam.EnvironmentName);
        result.Add("Context", logParam.Context);
        result.Add("CreationDate", logParam.CreationDate);
        result.Add("Category", $"{logParam.Category}");
        result.Add("HostName", logParam.HostName);
        result.Add("LogId", logParam.LogId);
        foreach (var item in logParam.ExtendedParameterList)
        {
            if (string.IsNullOrWhiteSpace(item.Value))
			{
                continue;
			}
            result.Add(item.Key, item.Value);
        }
        return result;
    }
}
