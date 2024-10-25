namespace RHWebApplication.Web.Services;

using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class EmployeeService
{
    private readonly HttpClient _httpClient;

    public EmployeeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<List<EmployeeResponse>> GetEmployees()
    {
        var response = await _httpClient.GetFromJsonAsync<List<EmployeeResponse>>("/Employee");
        return response;
    }

    public async Task<HttpResponseMessage> PostEmployee(EmployeeRequest employee)
    {
        var response = await _httpClient.PostAsJsonAsync("/Employee", employee);
        return response;
    }

    public async Task<List<string>?> GetEmployeeNames()
    {
        var response = await _httpClient.GetFromJsonAsync<List<String>>("/Employee/Names");
        return response;
    }
}