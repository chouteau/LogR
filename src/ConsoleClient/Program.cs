using LogRPush;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogRPush(logSettings =>
{
    logSettings.HostName = "Console";
    logSettings.LogLevel = LogLevel.Debug;
    logSettings.LogServerUrlList.Add("http://localhost:5276/");
});

var app = builder.Build();

app.Services.UseLogRPush();

await app.StartAsync();

var logger = app.Services.GetService<ILogger<Program>>();

while (true)
{
    await Task.Delay(3 * 1000);
	logger.LogCritical(new Exception("Critical1\nCritical2\n"), "Critical : {0}", DateTime.Now);
	var error = new Exception("Error1\nError2\n");
	error.Data.Add("Param1", "Value1");
	error.Data.Add("Param2", "Value2");
	logger.LogError(error, "Error : {Now}", DateTime.Now);
	logger.LogWarning("Warning : {0}", DateTime.Now);
	logger.LogInformation("Info : {0}", DateTime.Now);
	logger.LogDebug("Debug : {0}", DateTime.Now);
	logger.LogTrace("Trace : {0}", DateTime.Now);
}

Console.ReadKey();


