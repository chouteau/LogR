using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogRWebMonitor.Components
{
    public partial class Monitor
    {
        [Inject]
        protected LogCollector LogCollector { get; set; }

        public IEnumerable<LogRPush.LogInfo> LogInfoList { get; set; }

        protected override void OnInitialized()
        {
            LogCollector.OnChanged += async () =>
            {
                UpdateLists();
                await InvokeAsync(() =>
                {
                    base.StateHasChanged();
                });
            };
            UpdateLists();
        }

        void UpdateLists()
        {
            LogInfoList = LogCollector
                                .GetLogInfoList()
                                .Take(500);
        }
    }
}
