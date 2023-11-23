using System.Runtime.CompilerServices;
using System.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace LogRWebMonitor.Components;

public sealed partial class Monitor : ComponentBase, IDisposable
{
    [Inject] 
    LogCollector LogCollector { get; set; } = default!;
    
    [Inject] 
    NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    IJSRuntime JSRuntime { get; set; } = default!;

    [Parameter] 
    public LogRPush.Category? MinimumLevel { get; set; } = LogRPush.Category.Info;

	[Parameter]
    public EventCallback<LogRPush.LogInfo> LogAdded { get; set; }

    [Parameter]
    public int DisplayLogLimit { get; set; } = 500;

	LogFilter filter = new();
    List<LogRPush.LogInfo> logInfoList = new();
    bool insertLogs = true;
    bool isInitialized = false;
    List<string> hostList = new();
    List<string> machineNameList = new();
    List<string> applicationNameList = new();
    List<string> contextList = new();
	string? currentLogId;

    protected override void OnInitialized()
    {
        if (isInitialized)
        {
            return;
        }
        isInitialized = true;
        ResetFilter();

		NavigationManager.LocationChanged += NavigationManager_LocationChanged;
		LogCollector.OnAddLog += LogCollector_OnAddLog;

        UpdateLists();
    }

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			await BindFilter();
		}
	}

	async void NavigationManager_LocationChanged(object? sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
	{
		var logid = currentLogId;
		await BindFilter();
		if (currentLogId != logid)
		{
			StateHasChanged();
		}
	}

	void LogCollector_OnAddLog(LogRPush.LogInfo log)
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
			if (logInfoList.Count > DisplayLogLimit)
			{
				var last = logInfoList.Last();
				logInfoList.Remove(last);
			}
			logInfoList.Insert(0, log);

			if (!hostList.Contains(log.HostName))
			{
				hostList.Add(log.HostName);
			}

			if (!machineNameList.Contains(log.MachineName))
			{
				machineNameList.Add(log.MachineName);
			}

			if (!applicationNameList.Contains(log.ApplicationName))
			{
				applicationNameList.Add(log.ApplicationName);
			}

			if (!contextList.Contains(log.Context))
			{
				contextList.Add(log.Context);
			}

			InvokeAsync(StateHasChanged);
		}

		if (LogAdded.HasDelegate)
		{
			InvokeAsync(() =>
			{
				LogAdded.InvokeAsync(log);
			});
		}
	}

	async void EditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
	{
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
	}

	void ToggleInsert()
	{
        insertLogs = !insertLogs;
    }

    async Task BindFilter()
	{
		var filterJson = await GetFromLocalStorage("LogRWebMonitor.Filter");
		if (!string.IsNullOrWhiteSpace(filterJson))
		{
			var f = System.Text.Json.JsonSerializer.Deserialize<LogFilter>(filterJson);
			if (f is not null)
			{
				filter = f;
			}
		}
        var uri = new Uri($"{NavigationManager.ToAbsoluteUri(NavigationManager.Uri)}");
        var query = HttpUtility.ParseQueryString(uri.Query);
        filter.Search = filter.Search ?? query.Get("s");
        filter.ApplicationName = filter.ApplicationName ?? query.Get("a");
        filter.HostName = filter.HostName ?? query.Get("h");
        filter.MachineName = filter.MachineName ?? query.Get("m");
        filter.Context = filter.Context ?? query.Get("c");
        currentLogId = currentLogId ?? query.Get("l");
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
    }

    void UpdateLists()
    {
        logInfoList = LogCollector.GetLogInfoList(filter).ToList();
        base.StateHasChanged();
    }

    async Task ApplyFilter()
	{
        await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
        UpdateLists();
    }

    async Task LevelChanged(CheckedLevel item, MouseEventArgs args)
    {
        item.Checked = !item.Checked;
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
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

	public async Task SaveToLocalStorage(string key, string value)
	{
		await JSRuntime.InvokeVoidAsync("blazorExtensions.WriteLocalStorage", key, value);
	}

	public async Task<string> GetFromLocalStorage(string key)
	{
		return await JSRuntime.InvokeAsync<string>("blazorExtensions.ReadLocalStorage", key);
	}

    public void Dispose()
    {
        LogCollector.OnAddLog -= LogCollector_OnAddLog;
		NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
    }

}
