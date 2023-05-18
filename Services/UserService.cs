using MessengerApp.Models;
using MessengerApp.Services.Responses;
using System.Net.Http.Json;

namespace MessengerApp.Services;
public class UserService : IUserService
{
    private HttpClient httpClient;
    public UserService(HttpClient httpClient, string uri)
    {
        this.httpClient = httpClient;
        _serverRootUrl = uri;
    }
    private string _serverRootUrl;
    public async Task<ContentResponse<User>> GetUserInfoAsync(string id)
    {
        var result = new ContentResponse<User>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_serverRootUrl}/api/user/getuserinfo/{id}");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else 
                result.Content = await response.Content.ReadFromJsonAsync<User>();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<string>> SetAvatarOfUserAsync(MultipartFormDataContent file) //TODO
    {
        var result = new ContentResponse<string>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{_serverRootUrl}/api/user/ava");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            result.StatusMessage = await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<BaseResponse> DeleteUserAsync()
    {
        var result = new BaseResponse();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, $"{_serverRootUrl}/api/user/deleteuser");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            result.StatusMessage = await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<MultipartFormDataContent>> GetUserAvatarAsync(string id)
    {
        // var result = new Con
        throw new NotImplementedException();
    }
    

}