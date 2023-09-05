using LogRWebMonitor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using OvhGrayLogR;

const string APPLICATION_NAME = "LogRWebApp";

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection(APPLICATION_NAME);
var logRSettings = new LogRWebApp.Services.LogRWebAppSettings();
section.Bind(logRSettings);
builder.Services.AddSingleton(logRSettings);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = APPLICATION_NAME;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(15);
                    options.Cookie.HttpOnly = true;
                });

var emailTemplatesFolder = System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location);
emailTemplatesFolder = System.IO.Path.Combine(emailTemplatesFolder, logRSettings.EmailTemplatesDirectoryName);
if (!System.IO.Directory.Exists(emailTemplatesFolder))
{
    System.IO.Directory.CreateDirectory(emailTemplatesFolder);
}

var fluentEmailBuilder = builder.Services.AddFluentEmail(logRSettings.SenderEmail)
                                    .AddRazorRenderer(emailTemplatesFolder);

if (logRSettings.EmailProviderName == "sendgrid")
{
    fluentEmailBuilder.AddSendGridSender(logRSettings.SendGridApiKey, false);
}

var folder = new System.IO.DirectoryInfo(logRSettings.StoreFolder);
if (!folder.Exists)
{
    System.Diagnostics.Trace.WriteLine($"Create store folder {folder.FullName}");
    Directory.CreateDirectory(folder.FullName);
}
builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(folder)
        .SetApplicationName(APPLICATION_NAME)
        .SetDefaultKeyLifetime(TimeSpan.FromDays(60));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddHealthChecks();

builder.AddLogRWebMonitor(cfg =>
{
    if (builder.Environment.IsDevelopment())
    {
        cfg.LogLevel = LogLevel.Trace;
    }
    cfg.HostName = "IIS";
    var keywordsSettings = builder.Configuration.GetSection("LogRWebApp:KeywordMessageFilters");
    if (keywordsSettings.Exists())
    {
        cfg.KeywordMessageFilters = keywordsSettings.Get<string[]>().ToList();
    }
});

if (logRSettings.UseOvhGrayLogRPlugin)
{
    builder.AddOvhGrayLogR();
}

builder.Services.AddSingleton<LogRWebApp.Services.AdminLoginContext>();

var app = builder.Build();

if (logRSettings.UseOvhGrayLogRPlugin)
{
    var graylog = app.Services.GetRequiredService<OvhGraylogLogger>();
    app.UseLogRWebMonitor(l =>
    {
        try
        {
            graylog.SendLog(l);
        }
        catch { /* on error resume next */ }
    });
} 
else
{
    app.UseLogRWebMonitor();
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapHealthChecks("/healthcheck");

app.Run();
