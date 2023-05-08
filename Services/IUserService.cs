using MessengerApp.Models;
namespace MessengerApp.Services;
public interface IUserService
{
    Task<User?> GetUserInfoAsync(string id); // null if user not found
    Task<string?> SetAvatarOfUserAsync(MultipartFormDataContent file); //return URI to download avatar
    Task<bool> DeleteUserAsync(); //true with success
    Task<MultipartFormDataContent?> GetUserAvatarAsync(string id); //null if user not found or user doesn't have avatar
    
}