using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor.Components
{
    public partial class Monitor : ComponentBase
    {
        [Inject] LogCollector LogCollector { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        LogFilter filter = new();
        IEnumerable<LogRPush.LogInfo> logInfoList;

        protected override void OnInitialized()
        {
            LogCollector.OnChanged += async () =>
            {
                await InvokeAsync(() =>
                {
                    UpdateLists();
                });
            };
            UpdateLists();
        }

        void UpdateLists()
        {
            logInfoList = LogCollector.GetLogInfoList(filter);
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
