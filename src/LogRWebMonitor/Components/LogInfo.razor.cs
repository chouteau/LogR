using Microsoft.AspNetCore.Components;

namespace LogRWebMonitor.Components
{
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
                switch (logInfo.Category)
                {
                    case LogRPush.Category.Trace:
                        return "table-secondary";
                    case LogRPush.Category.Debug:
                        return "table-default";
                    case LogRPush.Category.Error:
                        return "table-danger";
                    case LogRPush.Category.Fatal:
                        return "table-dark";
                    case LogRPush.Category.Info:
                        return "table-info";
                    case LogRPush.Category.Notification:
                        return "table-success";
                    case LogRPush.Category.Sql:
                        return "table-primary";
                    case LogRPush.Category.Warn:
                        return "table-warning";
                    default:
                        return string.Empty;
                }
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
            base.OnInitialized();
        }

        void ShowLogs()
        {
            var uri = new Uri(NavigationManager.Uri);
            var prms = System.Web.HttpUtility.ParseQueryString(uri.Query);
            var lvalue = prms["l"];
            var navigateTo = NavigationManager.Uri.Replace($"l={lvalue}", "").TrimEnd('?').TrimEnd('&');
            NavigationManager.NavigateTo(navigateTo);
        }
    }
}