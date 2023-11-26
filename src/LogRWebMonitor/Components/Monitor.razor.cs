using System.ComponentModel;
using System.Web;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;

namespace LogRWebMonitor.Components;

public sealed partial class Monitor : ComponentBase, IDisposable
{
	[Inject]
	LogCollector LogCollector { get; set; } = default!;

	[Inject]
	NavigationManager NavigationManager { get; set; } = default!;

	[Inject]
	ILogger<Monitor> Logger { get; set; } = default!;

	[Inject]
	IJSRuntime JSRuntime { get; set; } = default!;

	[Inject]
	IMemoryCache Cache { get; set; } = default!;

	[Parameter]
	public LogRPush.Category? MinimumLevel { get; set; } = LogRPush.Category.Info;

	[Parameter]
	public EventCallback<LogRPush.LogInfo> LogAdded { get; set; }

	[Parameter]
	public int DisplayLogLimit { get; set; } = 500;

	LogFilter filter = new();
	BindingList<LogRPush.LogInfo> logInfoList = new();
	bool insertLogs = true;
	bool isInitialized = false;
	List<FilterItem> hostList = new();
	List<FilterItem> machineNameList = new();
	List<FilterItem> applicationNameList = new();
	List<FilterItem> contextList = new();
	string? currentLogId;

	protected override void OnInitialized()
	{
		if (isInitialized)
		{
			return;
		}
		isInitialized = true;

		NavigationManager.LocationChanged += NavigationManager_LocationChanged;
		LogCollector.OnAddLog += LogCollector_OnAddLog;

		LoadList();
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
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
			AddFilterToQueryString();
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

		var machineNameFilter = filter.AllMachine
								|| filter.MachineNameList.Exists(i => i.Equals(log.MachineName, StringComparison.InvariantCultureIgnoreCase));

		var contextFilter = filter.AllContext
							|| filter.ContextList.Exists(i => i.Equals(log.Context, StringComparison.InvariantCultureIgnoreCase));

		var hostNameFilter = filter.AllHost
							|| filter.HostNameList.Exists(i => i.Equals(log.HostName, StringComparison.InvariantCultureIgnoreCase));

		var applicationNameFilter = filter.AllApplication
									|| filter.ApplicationNameList.Exists(i => i.Equals(log.ApplicationName, StringComparison.InvariantCultureIgnoreCase));

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

			if (!hostList.Exists(i => i.Name.Equals(log.HostName, StringComparison.InvariantCultureIgnoreCase)))
			{
				hostList.Add(new FilterItem
				{
					IsSelected = true,
					Name = log.HostName
				});
			}

			if (!machineNameList.Exists(i => i.Name.Equals(log.MachineName, StringComparison.InvariantCultureIgnoreCase)))
			{
				machineNameList.Add(new FilterItem
				{
					IsSelected = true,
					Name = log.MachineName
				});
			}

			if (!applicationNameList.Exists(i => i.Name.Equals(log.ApplicationName, StringComparison.InvariantCultureIgnoreCase)))
			{
				applicationNameList.Add(new FilterItem
				{
					IsSelected = true,
					Name = log.ApplicationName
				});
			}

			if (!contextList.Exists(i => i.Name.Equals(log.Context, StringComparison.InvariantCultureIgnoreCase)))
			{
				contextList.Add(new FilterItem
				{
					IsSelected = true,
					Name = log.Context
				});
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
		var uri = new Uri($"{NavigationManager.ToAbsoluteUri(NavigationManager.Uri)}");
		var query = HttpUtility.ParseQueryString(uri.Query);
		currentLogId = currentLogId ?? query.Get("l");
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
	}

	void LoadList()
	{
		var il = insertLogs;
		insertLogs = false;
		logInfoList.Clear();
		var loglist = LogCollector.GetLogInfoList(filter);
		foreach (var log in loglist)
		{
			logInfoList.Add(log);
		}
		StateHasChanged();
		insertLogs = il;
	}

	async Task ApplyFilter()
	{
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
		LoadList();
	}

	async Task LevelChanged(CheckedLevel item, MouseEventArgs args)
	{
		item.Checked = !item.Checked;
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
		LoadList();
	}

	async Task ResetFilter()
	{
		filter = new();
		if (MinimumLevel != null)
		{
			foreach (var item in filter.LevelList.Where(i => i.Value < MinimumLevel))
			{
				item.Checked = false;
			}
		}
		await SaveToLocalStorage("LogRWebMonitor.Filter", System.Text.Json.JsonSerializer.Serialize(filter));
	}

	public async Task SaveToLocalStorage(string key, string value)
	{
		try
		{
			await JSRuntime.InvokeVoidAsync("blazorExtensions.WriteLocalStorage", key, value);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, ex.Message);
		}
	}

	public async Task<string?> GetFromLocalStorage(string key)
	{
		try
		{
			return await JSRuntime.InvokeAsync<string>("blazorExtensions.ReadLocalStorage", key);
		}
		catch (Exception ex)
		{
			Logger.LogError(ex, ex.Message);
		}
		return null;
	}

	void AddFilterToQueryString()
	{
		if (filter is null)
		{
			return;
		}

		var guid = Guid.NewGuid();
		var key = $"LogRFilter_{guid}";

		Cache.Set(key, filter, DateTime.Now.AddHours(1));

		var navigateToUri = NavigationManager.GetUriWithQueryParameter("logrfiltr", $"{guid}");
		NavigationManager.NavigateTo(navigateToUri);
	}

	async Task ToogleAllHost()
	{
		filter.HostNameList.Clear();
		filter.AllHost = !filter.AllHost;
		if (!filter.AllHost)
		{
			filter.HostNameList = hostList.Where(i => i.IsSelected).Select(i => i.Name).Distinct().ToList();
		}
		await ApplyFilter();
	}

	async Task AddOrRemoveHost(FilterItem item)
	{
		if (item.IsSelected
			&& !filter.HostNameList.Contains(item.Name))
		{
			filter.HostNameList.Add(item.Name);
		}
		else if (!item.IsSelected
			&& filter.HostNameList.Contains(item.Name))
		{
			filter.HostNameList.Remove(item.Name);
		}
		await ApplyFilter();
	}

	async Task ToogleAllMachine()
	{
		filter.MachineNameList.Clear();
		filter.AllMachine = !filter.AllMachine;
		if (!filter.AllMachine)
		{
			filter.MachineNameList = machineNameList.Where(i => i.IsSelected).Select(i => i.Name).Distinct().ToList();
		}
		await ApplyFilter();
	}

	async Task AddOrRemoveMachine(FilterItem item)
	{
		if (item.IsSelected
			&& !filter.MachineNameList.Contains(item.Name))
		{
			filter.MachineNameList.Add(item.Name);
		}
		else if (!item.IsSelected
			&& filter.MachineNameList.Contains(item.Name))
		{
			filter.MachineNameList.Remove(item.Name);
		}
		await ApplyFilter();
	}

	async Task ToogleAllContext()
	{
		filter.ContextList.Clear();
		filter.AllContext = !filter.AllContext;
		if (!filter.AllContext)
		{
			filter.ContextList = contextList.Where(i => i.IsSelected).Select(i => i.Name).Distinct().ToList();
		}
		await ApplyFilter();
	}

	async Task AddOrRemoveContext(FilterItem item)
	{
		if (item.IsSelected
			&& !filter.ContextList.Contains(item.Name))
		{
			filter.ContextList.Add(item.Name);
		}
		else if (!item.IsSelected
			&& filter.ContextList.Contains(item.Name))
		{
			filter.ContextList.Remove(item.Name);
		}
		await ApplyFilter();
	}

	async Task ToogleAllApplication()
	{
		filter.ApplicationNameList.Clear();
		filter.AllApplication = !filter.AllApplication;
		if (!filter.AllApplication)
		{
			filter.ApplicationNameList = applicationNameList.Where(i => i.IsSelected).Select(i => i.Name).Distinct().ToList();
		}
		await ApplyFilter();
	}

	async Task AddOrRemoveApplication(FilterItem item)
	{
		if (item.IsSelected
			&& !filter.ApplicationNameList.Contains(item.Name))
		{
			filter.ApplicationNameList.Add(item.Name);
		}
		else if (!item.IsSelected
			&& filter.ApplicationNameList.Contains(item.Name))
		{
			filter.ApplicationNameList.Remove(item.Name);
		}
		await ApplyFilter();
	}



	void ClearList()
	{
		logInfoList.Clear();
	}

	public void Dispose()
	{
		LogCollector.OnAddLog -= LogCollector_OnAddLog;
		NavigationManager.LocationChanged -= NavigationManager_LocationChanged;
		logInfoList.Clear();
		GC.SuppressFinalize(this);
	}

}
