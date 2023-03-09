# LogRPush (1.1.11.0)
Realtime Logger with blazor server

## Where can I get it ?

**First**, install [LogRPush](https://www.nuget.org/packages/LogRPush) from the package manager console in your app.

> PM> Install-Package LogRPush

LogRPush logging for dotnet in real time.

Next configure LogRPush in Program

```csharp

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogRPush(logSettings =>
{
    logSettings.HostName = "Console";
    logSettings.LogLevel = LogLevel.Debug;
    logSettings.LogServerUrlList.Add("http://example.logrserver.com/");
});

var app = builder.Build();

app.Services.UseLogRPush();

```

Install your own LogRServer https://github.com/chouteau/LogR/releases/tag/Latest on IIS

or

Install your own LogRServer with docker https://github.com/chouteau/LogR/pkgs/container/logr%2Flogrpushserver

![LogRServer](/doc/logrserver.gif)