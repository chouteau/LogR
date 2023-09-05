using Microsoft.AspNetCore.Components;

namespace LogRWebMonitor.Components
{
    public partial class FilterLink
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; } = default!;

        [Parameter]
        public string? MachineName { get; set; }

        [Parameter]
        public string? MachineNameFilter { get; set; }

        [Parameter]
        public string? HostName { get; set; }

        [Parameter]
        public string? HostNameFilter { get; set; }

        [Parameter]
        public string? Context { get; set; }

        [Parameter]
        public string? ContextFilter { get; set; }

        [Parameter]
        public string? ApplicationName { get; set; }

        [Parameter]
        public string? ApplicationNameFilter { get; set; }

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        void ShowFilteredLogs()
        {
            var parameters = new Dictionary<string, object?>();
            AddParameter(parameters, "a", ApplicationNameFilter, ApplicationName);
            AddParameter(parameters, "h", HostNameFilter, HostName);
            AddParameter(parameters, "m", MachineNameFilter, MachineName);
            AddParameter(parameters, "c", ContextFilter, Context);
            var result = NavigationManager.GetUriWithQueryParameters(NavigationManager.Uri, new System.Collections.ObjectModel.ReadOnlyDictionary<string, object?>(parameters));
            NavigationManager.NavigateTo(result, true);
        }

        public void AddParameter(Dictionary<string, object?> parameters, string paramterName, string? filter, string? value)
        {
            if (!string.IsNullOrWhiteSpace(filter))
            {
                if (filter == value)
                {
                    parameters.Add(paramterName, $"!{value}");
                }
                else
                {
                    parameters.Add(paramterName, value);
                }
            }
            else
            {
                parameters.Add(paramterName, value);
            }
        }
    }
}