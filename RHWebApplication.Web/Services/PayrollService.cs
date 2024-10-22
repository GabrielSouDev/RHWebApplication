namespace RHWebApplication.Web.Services;

using RHWebApplication.Shared.Models.PayrollModels;
using RHWebApplication.Web.Requests;
using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
public class PayrollService
{
    private readonly HttpClient _httpClient;

    public PayrollService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> PostPayroll(PayrollRequest payroll)
    {
        var response = await _httpClient.PostAsJsonAsync("/Payroll", payroll);
        return response;
    }
    public async Task<List<PayrollResponse>> GetPayrolls()
    {
        var response = await _httpClient.GetFromJsonAsync<List<PayrollResponse>>("/Payroll");
        return response;
    }
    public async Task<HttpResponseMessage> DeletePayroll(int id)
    {
        var response = await _httpClient.DeleteAsync($"/Payroll/{id}");
        return response;
    }
}