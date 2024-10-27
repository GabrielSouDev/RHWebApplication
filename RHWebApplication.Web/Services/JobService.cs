namespace RHWebApplication.Web.Services;

using Azure;
using RHWebApplication.Shared.Models.CompanyModels;
using RHWebApplication.Shared.Models.UserModels;
using RHWebApplication.Web.Requests;
using RHWebApplication.Web.Responses;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class JobService
{
    private readonly HttpClient _httpClient;

    public JobService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<string>?> GetJobTitlesByCompany(string company)
    {
        var response = await _httpClient.GetFromJsonAsync<List<String>>($"/Job/Titles/{company}");
        return response;
    }
    public async Task<List<JobResponse>?> GetJobListByCorpany(string company)
    {
        var response = await _httpClient.GetFromJsonAsync<List<JobResponse>>($"/Job/{company}");
        return response;
    }
    public async Task<HttpResponseMessage> PutJob(JobRequest job)
    {
        var response = await _httpClient.PutAsJsonAsync("/job", job);
        return response;
    }
    public async Task<HttpResponseMessage> PostJob(JobRequest job)
    {
        var response = await _httpClient.PostAsJsonAsync("/Job", job);
        return response;
    }
    public async Task<HttpResponseMessage> DeleteJob(int id)
    {
        var response = await _httpClient.DeleteAsync($"/job/{id}");
        return response;
    }
}