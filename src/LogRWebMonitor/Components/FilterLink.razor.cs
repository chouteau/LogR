using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LogRWebMonitor.Components;

public partial class FilterLink
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;

    [Parameter]
    public FilterItem FilterItem { get; set; } = default!;

    [Parameter]
    public EventCallback FilterItemChanged { get; set; }

    void ApplyFilter(ChangeEventArgs arg)
    {
        FilterItem.IsSelected = !FilterItem.IsSelected;
        if (FilterItemChanged.HasDelegate)
        {
            FilterItemChanged.InvokeAsync();
		}
    }
}