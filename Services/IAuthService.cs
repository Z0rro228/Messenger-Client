using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IAuthService
{ //Return true, if success
    Task<BaseResponse> RegisterAsync(string Email, string UserName, string Password);
    Task<BaseResponse> LoginAsync(string UserName, string Password);
    Task<BaseResponse> LogoutAsync();
}