# LogR (1.4.14.5)
Realtime Logger with windows monitor 

## Where can I get it ?

**First**, install [LogR](https://www.nuget.org/packages/LogR) from the package manager console in your app.

> PM> Install-Package LogR

LogRCore logging for dotnetcore.

```csharp

public class Startup
{
	public Startup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	public IConfiguration Configuration { get; }

	public void ConfigureServices(IServiceCollection services)
	{
		var logRConfig = new LogRCore.LogRConfiguration()
		{
			LogLevel = LogLevel.Debug,
			ApplicationName = "YourApplicationName",
			EndPoint = $"/logger",
			HostName = "WebApp"
		};

		services.ConfigureLogR(logRConfig);
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
	{
		app.UseLogR(loggerFactory);
	}
}
```