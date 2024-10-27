namespace RHWebApplication.Web.Services;

using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Web.Requests;
using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class CompanyService
{
    private readonly HttpClient _httpClient;

    public CompanyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> PostCompany(CompanyRequest company)
    {
        var response = await _httpClient.PostAsJsonAsync("/Company", company);
        return response;
    }
    public async Task<List<string>?> GetCompanyNames()
    {
        var response = await _httpClient.GetFromJsonAsync<List<string>>("/Company/Names");
        return response;
    }

}