using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OvhGrayLogR
{
	public static class StartupExtension
	{
		public static WebApplicationBuilder AddOvhGrayLogR(this WebApplicationBuilder builder)
		{
			var settings = new GrayLoggerConfiguration();
			var section = builder.Configuration.GetSection("GrayLoggerConfiguration");
			section.Bind(settings);

			builder.Services.AddSingleton(settings);

			builder.Services.AddSingleton<OvhGraylogLogger>();
			return builder;

		}
	}
}