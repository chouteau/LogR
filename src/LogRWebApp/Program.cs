
using LogRWebMonitor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using OvhGrayLogR;

const string APPLICATION_NAME = "LogRWebApp";

var builder = WebApplication.CreateBuilder(args);

var palaceSection = builder.Configuration.GetSection(APPLICATION_NAME);
var logRSettings = new LogRWebApp.Services.LogRWebAppSettings();
palaceSection.Bind(logRSettings);
builder.Services.AddSingleton(logRSettings);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = APPLICATION_NAME;
                    options.SlidingExpiration = true;
                    options.ExpireTimeSpan = TimeSpan.FromDays(15);
                    options.Cookie.HttpOnly = true;
                });

var folder = System.IO.Path.Combine(System.IO.Path.GetTempPath(), APPLICATION_NAME);
if (!Directory.Exists(folder))
{
    Directory.CreateDirectory(folder);
}
builder.Services.AddDataProtection()
        .PersistKeysToFileSystem(new System.IO.DirectoryInfo(folder))
        .SetApplicationName(APPLICATION_NAME)
        .SetDefaultKeyLifetime(TimeSpan.FromDays(60));

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMemoryCache();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();

builder.AddLogRWebMonitor(cfg =>
{
    if (builder.Environment.IsDevelopment())
    {
        cfg.LogLevel = LogLevel.Trace;
    }
    cfg.HostName = "IIS";
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

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseLogRWebMonitor();

app.Run();
