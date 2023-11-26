using LogRPush;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogRPush(logSettings =>
{
    logSettings.HostName = "Console";
    logSettings.LogServerUrlList.Add("http://localhost:5276/");
});

builder.Services.AddSingleton<VerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLong>();

var app = builder.Build();

app.Services.UseLogRPush();

await app.StartAsync();

var c = app.Services.GetRequiredService<VerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLong>();

await c.WriteLogs();

Console.ReadKey();

public class VerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLong
{
	private ILogger _logger;
	public VerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLong(ILogger<VerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLongVerryVerrySuperLong> logger)
	{
		_logger = logger;
	}

	public async Task WriteLogs()
	{
		while (true)
		{
			await Task.Delay(3 * 1000);
			_logger.LogCritical(new Exception("Critical1\nCritical2\n"), "Critical : {0}", DateTime.Now);
			var error = new Exception(System.Environment.StackTrace);
			error.Data.Add("Param1", "Value1");
			error.Data.Add("Param2", "Value2");
			_logger.LogError(error, "Error : {Now}", DateTime.Now);
			_logger.LogWarning("Warning : {0}", DateTime.Now);
			_logger.LogInformation("Lorem Ipsum is simply dummy text of the printing and typesetting industry.Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
			_logger.LogDebug("Debug : {0}", DateTime.Now);
			_logger.LogTrace("Trace : {0}", DateTime.Now);
			_logger.LogInformation("Do not display this message test33");
			_logger.LogInformation("Tags {a} {b} {c} {d}", 1, 2, 3, 4);
		}

	}
}
