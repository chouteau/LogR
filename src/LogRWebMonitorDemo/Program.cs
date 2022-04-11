using LogRWebMonitor;
using LogRWebMonitorDemo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddHostedService<LogRWebMonitorDemo.FakeLogWriter>();

builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

builder.AddLogRWebMonitor(logSettings => 
{
	logSettings.HostName = "Demo";
	logSettings.LogLevel = LogLevel.Trace;
	logSettings.LogCountMax = 50;
	logSettings.EnvironmentName = builder.Environment.EnvironmentName;
});

var app = builder.Build();

app.UseLogRWebMonitor(l =>
{
	Console.WriteLine(l.Message);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
