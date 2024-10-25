namespace RHWebApplication.Web.Services;

using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Web.Requests;
using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class AdminService
{
    private readonly HttpClient _httpClient;

    public AdminService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<HttpResponseMessage> PostAdmin(AdminRequest admin)
    {
        var response = await _httpClient.PostAsJsonAsync("/Admin", admin);
        return response;
    }
}