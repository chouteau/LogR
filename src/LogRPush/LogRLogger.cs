
namespace LogRPush;

public class LogRLogger : ILogger
{
	private readonly string _categoryName;

	public LogRLogger(LogRSettings logrSettings, string categoryName, IHttpClientFactory httpClientFactory)
	{
		this.LogRSettings = logrSettings;
		this._categoryName = categoryName;
		this.Semaphore = new SemaphoreSlim(1, 1);
		this.WriteQueue = new System.Collections.Concurrent.ConcurrentQueue<LogInfo>();
		this.HttpClientFactory = httpClientFactory;
	}

	protected LogRSettings LogRSettings { get; }
	protected SemaphoreSlim Semaphore { get; }
	protected System.Collections.Concurrent.ConcurrentQueue<LogInfo> WriteQueue { get; }
	protected IHttpClientFactory HttpClientFactory { get; }

	public IDisposable BeginScope<TState>(TState state)
	{
		return null;
	}

	public bool IsEnabled(LogLevel logLevel)
	{
		return LogRSettings.LogLevel <= logLevel;
	}

	public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
	{
		if (!IsEnabled(logLevel))
		{
			return;
		}

		var logInfo = new LogInfo()
		{
			ApplicationName = LogRSettings.ApplicationName,
			CreationDate = DateTime.Now,
			HostName = LogRSettings.HostName,
			MachineName = System.Environment.MachineName,
			Context = _categoryName,
			Message = $"{formatter(state, exception)}",
		};

		logInfo.ExceptionStack = GetExceptionContent(exception);

		switch (logLevel)
		{
			case LogLevel.Trace:
				logInfo.Category = Category.Debug;
				break;
			case LogLevel.Debug:
				logInfo.Category = Category.Debug;
				break;
			case LogLevel.Information:
				logInfo.Category = Category.Info;
				break;
			case LogLevel.Warning:
				logInfo.Category = Category.Warn;
				break;
			case LogLevel.Error:
				logInfo.Category = Category.Error;
				break;
			case LogLevel.Critical:
				logInfo.Category = Category.Fatal;
				break;
			case LogLevel.None:
				logInfo.Category = Category.Debug;
				break;
			default:
				break;
		}

		WriteQueue.Enqueue(logInfo);
		if (Semaphore.CurrentCount < 2)
		{
			Dequeue();
		}

	}

	private void Dequeue()
	{
		while (true)
		{
			bool result = WriteQueue.TryDequeue(out LogInfo logInfo);
			if (result)
			{
				WriteInternal(logInfo);
				continue;
			}
			break;
		}
	}

	private void WriteInternal(LogInfo logInfo)
	{
		Semaphore.Wait();
		foreach (var logServerUrl in LogRSettings.LogServerUrlList)
		{
			try
			{
				using var httpClient = HttpClientFactory.CreateClient("LogRClient");
				httpClient.BaseAddress = new Uri(logServerUrl);
				var httpMessage = new HttpRequestMessage(HttpMethod.Post, "/api/logging/writelog");
				httpMessage.Content = new StringContent(System.Text.Json.JsonSerializer.Serialize(logInfo), Encoding.UTF8, "application/json");
				var response = httpClient.Send(httpMessage);
				response.EnsureSuccessStatusCode();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			finally
			{
				Semaphore.Release();
			}
		}
		Dequeue();
	}

	private static string GetExceptionContent(Exception ex, int level = 0)
	{
		if (ex == null)
		{
			return null;
		}

		var content = new StringBuilder();
		content.Append("--------------------------------------------");
		content.AppendLine();
		content.AppendLine(ex.Message);
		content.AppendLine("--------------------------------------------");

		// Ajout des extensions d'erreur
		if (ex.Data != null
			&& ex.Data.Count > 0)
		{
			foreach (var item in ex.Data.Keys)
			{
				if (item != null && ex.Data != null && ex.Data[item] != null)
				{
					string data = string.Empty;
					try
					{
						data = ex.Data[item].ToString();
						content.AppendFormat("{0} = {1}", item, data);
					}
					catch { }
				}
				content.AppendLine();
			}
		}

		content.Append(ex.StackTrace);
		content.AppendLine();
		if (ex.InnerException != null)
		{
			content.Append("--------------------------------------------");
			content.AppendLine();
			content.Append("Inner Exception");
			content.AppendLine();
			content.Append(GetExceptionContent(ex.InnerException, level++));
		}
		return content.ToString();
	}

}

