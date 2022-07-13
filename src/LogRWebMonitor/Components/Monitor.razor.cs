using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogRWebMonitor.Components
{
    public partial class Monitor : ComponentBase
    {
        [Inject] LogCollector LogCollector { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        [Parameter] 
        public LogRPush.Category? MinimumLevel { get; set; } = LogRPush.Category.Info;

		[Parameter]
        public EventCallback<LogRPush.LogInfo> LogAdded { get; set; }

		LogFilter filter = new();
        IList<LogRPush.LogInfo> logInfoList;
        bool insertLogs = true;
        bool isInitialized = false;

        string currentLogId;

        protected override void OnInitialized()
        {
            if (isInitialized)
			{
                return;
			}
            isInitialized = true;
            ResetFilter();
            BindFilter();
            NavigationManager.LocationChanged += (s, arg) =>
            {
                var logid = currentLogId;
                BindFilter();
                if (currentLogId != logid)
				{
                    StateHasChanged();
				}
            };

            LogCollector.OnAddLog += async (log) =>
            {
                var level = filter.LevelList.SingleOrDefault(i => i.Value == log.Category && i.Checked);
                var searchFilter = filter.Search == null || LogCollector.IsMatchSearch(log, filter.Search);

                var machineNameFilter = log.MachineName == (filter.MachineName ?? log.MachineName);
                var contextFilter = log.Context == (filter.Context ?? log.Context);
                var hostNameFilter = log.HostName == (filter.HostName ?? log.HostName);
                var applicationNameFilter = log.ApplicationName == (filter.ApplicationName ?? log.ApplicationName);

                if (insertLogs
                    && level != null
                    && machineNameFilter
                    && contextFilter
                    && hostNameFilter
                    && applicationNameFilter
                    && searchFilter)
                {
                    await InvokeAsync(() =>
                    {
                        logInfoList.Insert(0, log);
                        StateHasChanged();
                    });
                }

                if (LogAdded.HasDelegate)
				{
                    await InvokeAsync(() =>
                    {
                        LogAdded.InvokeAsync(log);
                    });
                }
            };

            UpdateLists();
        }

		private void NavigationManager_LocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		void ToggleInsert()
		{
            insertLogs = !insertLogs;
        }

        void BindFilter()
		{
            var uri = new Uri($"{NavigationManager.ToAbsoluteUri(NavigationManager.Uri)}");
            var query = HttpUtility.ParseQueryString(uri.Query);
            filter.Search = query.Get("s");
            filter.ApplicationName = query.Get("a");
            filter.HostName = query.Get("h");
            filter.MachineName = query.Get("m");
            filter.Context = query.Get("c");
            currentLogId = query.Get("l");
        }

        void UpdateLists()
        {
            logInfoList = LogCollector.GetLogInfoList(filter).ToList();
            base.StateHasChanged();
        }

        void ApplyFilter()
		{
            UpdateLists();
        }

        void LevelChanged(CheckedLevel item, MouseEventArgs args)
        {
            item.Checked = !item.Checked;
            UpdateLists();
        }

        void ResetFilter()
		{
            filter = new();
            if (MinimumLevel != null)
            {
                foreach (var item in filter.LevelList.Where(i => i.Value < MinimumLevel))
                {
                    item.Checked = false;
                }
            }
        }
    }
}
