using MessengerApp.Models;
using MessengerApp.Services.Responses;
using System.Net.Http.Json;
using System.Net.Http.Headers;

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
    public async Task<ContentResponse<User>> GetUserInfoAsyncById(string id)
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
    public async Task<ContentResponse<User>> GetUserInfoAsyncByName(string userName)
    {
        var result = new ContentResponse<User>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_serverRootUrl}/api/user/getuserinfobyname/{userName}");
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
    public async Task<BaseResponse> SetAvatarOfUserAsync(Stream fileStream)
    {
        var result = new BaseResponse();
        try
        {
            using var multipartFormContent = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(fileStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "image.jpg");
            using var response = await httpClient.PostAsync($"{_serverRootUrl}/api/user/ava", multipartFormContent);

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

}