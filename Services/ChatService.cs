using MessengerApp.Models;
using MessengerApp.Services.Responses;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace MessengerApp.Services;
public class ChatService: IChatService
{
    private HttpClient httpClient;
    public ChatService(HttpClient httpClient, string uri)
    {
        this.httpClient = httpClient;
        _serverRootUrl = uri;
    }
    private string _serverRootUrl;

    public async Task<ContentResponse<Chat>> GetChatInfoAsync(int id)
    {
        var result = new ContentResponse<Chat>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/chat/getchatinfo/chatid=" + id.ToString());
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else 
                result.Content = await response.Content.ReadFromJsonAsync<Chat>();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<List<Chat>>> GetUsersChatsAsync()
    {
        var result = new ContentResponse<List<Chat>>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/chat/getuserschats");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else 
                result.Content = await response.Content.ReadFromJsonAsync<List<Chat>>();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;    
    }
    public async Task<BaseResponse> UploadChatAvatarAsync(int chatId, Stream fileStream)
    {
        var result = new BaseResponse();
        try
        {
            using var multipartFormContent = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(fileStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "image.jpg");
            using var response = await httpClient.PostAsync($"{_serverRootUrl}/api/chat/chatava/" + chatId.ToString(), multipartFormContent);

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