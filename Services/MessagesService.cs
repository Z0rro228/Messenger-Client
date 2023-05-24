using MessengerApp.Services.Responses;
using MessengerApp.Models;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace MessengerApp.Services;
public class MessagesService : IMessagesService
{
    private HttpClient httpClient;
    public MessagesService(HttpClient httpClient, string uri)
    {
        this.httpClient = httpClient;
        _serverRootUrl = uri;
    }
    private string _serverRootUrl;
    public async Task<ContentResponse<List<Message>>> GetMessagesRangeAsync(int chatId, int fromMsgId, int range)
    {
        
        var result = new ContentResponse<List<Message>>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + $"/api/messages/getmessagesrange/chatid={chatId}/frommsgid={fromMsgId}-range={range}");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else
                result.Content = await response.Content.ReadFromJsonAsync<List<Message>>();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<int>> GetLastReadMessageIdAsync(int chatId)
    {
        var result = new ContentResponse<int>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_serverRootUrl}/api/messages/getlastreadmessageid/chatid={chatId}");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                string strId = await response.Content.ReadAsStringAsync();
                result.Content = Int32.Parse(strId);  
            }
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<int>> GetNewestMessageIdAsync(int chatId)
    {
        var result = new ContentResponse<int>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"{_serverRootUrl}/api/messages/getnewestmessageid/chatid={chatId}");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else
            {
                string strId = await response.Content.ReadAsStringAsync();
                result.Content = Int32.Parse(strId);  
            }
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        // IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers;
        Console.WriteLine(httpClient.DefaultRequestHeaders.AsEnumerable().Count());
        // foreach(var i in headers)
        // {
        //     Console.WriteLine(i.Key);
        // }
        return result;
    }
    public async Task<BaseResponse> UploadFileAsync(Stream fileStream, FileResult res) 
    {
        var result = new BaseResponse();
        try
        {
            using var multipartFormContent = new MultipartFormDataContent();
            var fileStreamContent = new StreamContent(fileStream);
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(res.ContentType);
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: res.FileName);
            using var response = await httpClient.PostAsync($"{_serverRootUrl}/api/upload/attach", multipartFormContent);

            result.StatusCode = ((int)response.StatusCode);
            result.StatusMessage = await response.Content.ReadAsStringAsync();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<ContentResponse<List<Message>>> GetAllMessages(int chatId)
    {
        var result = new ContentResponse<List<Message>>();
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + $"/api/messages/getallmessages/chatid={chatId}");
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            if(result.StatusCode != 200 && result.StatusCode != 202)
            {
                result.StatusMessage = await response.Content.ReadAsStringAsync();
            }
            else
                result.Content = await response.Content.ReadFromJsonAsync<List<Message>>();
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }

}