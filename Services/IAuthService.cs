namespace MessengerApp.Services;
public interface IAuthService
{ //Return true, if success
    Task<bool> RegisterAsync(string Email, string UserName, string Password);
    Task<bool> LoginAsync(string UserName, string Password);
    Task<bool> LogoutAsync();
}