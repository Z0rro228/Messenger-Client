using System.Net.Http.Json;
using MessengerApp.Models;
using Newtonsoft.Json;
using System.Text;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;

// public class AuthService: IAuthService
// {
//     private HttpClient httpClient;
//     public AuthService()
//     {
//         httpClient = new HttpClient();
//     }
//     private string _serverRootUrl = "http://192.168.1.66:5296";
//     public async Task<BaseResponse?> RegisterAsync(string email, string userName, string password) //TODO: Test it
//     {
//         var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/auth/register");
//         var registerDTO = new RegisterDTO()
//         {
//             Email = email,
//             UserName = userName,
//             Password = password
//         };
//         string jsonContent = JsonConvert.SerializeObject(registerDTO);
//         var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
//         httpRequestMessage.Content = httpContent;
//         //Sending
//         var response = await httpClient.SendAsync(httpRequestMessage);
//         if(response?.StatusCode == System.Net.HttpStatusCode.Accepted)
//         {
//             return true;
//         }
//         return false;
//     }
//     public async Task<BaseResponse?> LoginAsync(string userName, string password)
//     {
//         var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/auth/register");
//         var loginDTO = new LoginDTO()
//         {
//             UserName = userName,
//             Password = password
//         };
//         string jsonContent = JsonConvert.SerializeObject(loginDTO);
//         var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json"); ;
//         httpRequestMessage.Content = httpContent;
//         //Sending
//         var response = await httpClient.SendAsync(httpRequestMessage);
//         if(response?.StatusCode == System.Net.HttpStatusCode.OK)
//         {
//             return true;
//         }
//         return false;
//     }
//     public async Task<BaseResponse?> LogoutAsync() //TODO: Test it
//     {
//         var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, _serverRootUrl + "/api/auth/logout");
//         //Sending
//         var response = await httpClient.SendAsync(httpRequestMessage);
//         if(response?.StatusCode == System.Net.HttpStatusCode.Accepted)
//             return true;
//         return false;
//     }
// }