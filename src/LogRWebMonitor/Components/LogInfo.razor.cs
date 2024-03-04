using Microsoft.AspNetCore.Components;

namespace LogRWebMonitor.Components;

public partial class LogInfo
{
    [Parameter]
    public string LogId { get; set; } = null!;

    [Inject]
    LogCollector LogCollector { get; set; } = default!;

    [Inject]
    NavigationManager NavigationManager { get; set; } = default!;

    LogRPush.LogInfo logInfo = new();
    string css
    {
        get
        {
            return logInfo.LogLevel switch
				{
					LogLevel.Trace => "table-secondary",
					LogLevel.Debug => "table-default",
					LogLevel.Error => "table-danger",
					LogLevel.Critical => "table-dark",
					LogLevel.Information => "table-info",
					LogLevel.Warning => "table-warning",
					_ => string.Empty,
				};
        }
    }

    MarkupString formatedExceptionStack
    {
        get
        {
            var stack = logInfo.ExceptionStack ?? string.Empty;
            return new MarkupString(stack.Replace("\n", "<br/>"));
        }
    }

    protected override void OnInitialized()
    {
        logInfo = LogCollector.GetLogInfo(LogId) ?? new();
    }

    void ShowLogs()
    {
        var uri = new Uri(NavigationManager.Uri);
        var prms = System.Web.HttpUtility.ParseQueryString(uri.Query);
        var lvalue = prms["l"];
        var navigateTo = NavigationManager.Uri.Replace($"l={lvalue}", "").TrimEnd('?').TrimEnd('&');
        NavigationManager.NavigateTo(navigateTo, true);
    }
}