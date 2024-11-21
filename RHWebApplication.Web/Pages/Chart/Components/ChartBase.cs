using Microsoft.AspNetCore.Components;
using RHWebApplication.Web.Pages.Charts;
using RHWebApplication.Web.Responses;

public abstract class ChartBase : ComponentBase, IChart
{
    public abstract bool IsLoading { get; set; }
    public abstract void LoadChart(List<PayrollResponse> payrollsSelected);
}
