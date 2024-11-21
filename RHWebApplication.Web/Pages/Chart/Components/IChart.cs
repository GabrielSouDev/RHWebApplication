using RHWebApplication.Web.Responses;

namespace RHWebApplication.Web.Pages.Charts;
public interface IChart
{
    void LoadChart(List<PayrollResponse> payrollsSelected);
}