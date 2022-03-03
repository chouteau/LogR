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

		LogFilter filter = new();
        IList<LogRPush.LogInfo> logInfoList;

        ElementReference tbody;

        protected override void OnInitialized()
        {
            if (MinimumLevel != null)
			{
				foreach (var item in filter.LevelList.Where(i => i.Value < MinimumLevel))
				{
                    item.Checked = false;
				}
			}

            var uri = new Uri($"{NavigationManager.ToAbsoluteUri(NavigationManager.Uri)}");
            var query = HttpUtility.ParseQueryString(uri.Query);

            filter.Search = query.Get("s");
            filter.ApplicationName = query.Get("a");
            filter.HostName = query.Get("h");
            filter.MachineName = query.Get("m");
            filter.Context = query.Get("c");

            LogCollector.OnAddLog += async (log) =>
            {
                await InvokeAsync(() =>
                {
                    var level = filter.LevelList.SingleOrDefault(i => i.Value == log.Category && i.Checked);
                    if (level != null)
					{
                        logInfoList.Insert(0, log);
                        StateHasChanged();
                    }
                });
            };

            UpdateLists();
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
    }
}
