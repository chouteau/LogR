using Microsoft.AspNetCore.Components;

namespace LogRWebMonitor.Components
{
    public partial class LogRow
    {
        [Parameter]
        public LogRPush.LogInfo Log { get; set; } = default!;

        [Parameter]
        public LogFilter Filter { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        LogCollector LogCollector { get; set; } = default!;

        string css
        {
            get
            {
                switch (Log.Category)
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

        void ShowLog()
        {
            var parameters = new Dictionary<string, object?>();
            parameters.Add("l", Log.LogId);
            var uri = NavigationManager.GetUriWithQueryParameters(NavigationManager.Uri, new System.Collections.ObjectModel.ReadOnlyDictionary<string, object?>(parameters));
            LogCollector.AddLogDetail(Log);
            NavigationManager.NavigateTo(uri);
        }
    }
}