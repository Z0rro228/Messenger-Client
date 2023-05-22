using System.Net.Http.Json;
using MessengerApp.Models;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using MessengerApp.Services.Responses;
using System.Diagnostics;

namespace MessengerApp.Services;

public class AuthService: IAuthService
{
    private HttpClient httpClient;
    private CookieContainer _authCookies;
    public AuthService(HttpClient httpClient, string uri, CookieContainer authCookies)
    {
        this.httpClient = httpClient;
        _serverRootUrl = uri;
        _authCookies = authCookies;
    }
    private string _serverRootUrl;
    public async Task<BaseResponse> RegisterAsync(string email, string userName, string password) 
    {
        var registerDTO = new RegisterDTO()
        {
            Email = email,
            UserName = userName,
            Password = password
        };
        var result = new BaseResponse();
        //Sending
        try
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _serverRootUrl + "/api/auth/register");
            string jsonContent = JsonConvert.SerializeObject(registerDTO);
            var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
            httpRequestMessage.Content = httpContent;
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
    public async Task<BaseResponse> LoginAsync(string userName, string password)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, _serverRootUrl + "/api/auth/login");
        var loginDTO = new LoginDTO()
        {
            UserName = userName,
            Password = password
        };
        string jsonContent = JsonConvert.SerializeObject(loginDTO);
        var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
        httpRequestMessage.Content = httpContent;
        var result = new BaseResponse();
        //Sending
        try
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);            
            result.StatusMessage = await response.Content.ReadAsStringAsync();
            // CookieContainer cookies = new CookieContainer();
            // получаем из запроса все элементы с заголовком Set-Cookie
            foreach (var cookieHeader in response.Headers.GetValues("Set-Cookie"))
                // добавляем заголовки кук в CookieContainer
            {
                _authCookies.SetCookies(new Uri(_serverRootUrl), cookieHeader);
                // Console.WriteLine(cookieHeader);
            }
            // _authCookies = cookies;
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
    public async Task<BaseResponse> LogoutAsync() //TODO: Test it
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/auth/logout");
        var result = new BaseResponse();
        //Sending
        try
        {
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
    public async Task<BaseResponse> GetTest()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/auth/test");
        var result = new BaseResponse();
        //Sending
        try
        {
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
    public async Task<ContentResponse<Chat>> GetChatInfoAsync(int id)
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/chat/getchatinfo/chatid=" + id.ToString());
        var result = new ContentResponse<Chat>();
        try
        {
            var response = await httpClient.SendAsync(httpRequestMessage);
            result.StatusCode = ((int)response.StatusCode);
            // result.Content = response.Content
        }
        catch(Exception ex)
        {
            result.StatusMessage = ex.Message;
        }
        return result;
    }
}