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
                return Log.LogLevel switch
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