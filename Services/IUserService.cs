using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IUserService
{
    Task<ContentResponse<User>> GetUserInfoAsync(string id); // null if user not found
    Task<ContentResponse<string>> SetAvatarOfUserAsync(MultipartFormDataContent file); //return URI to download avatar
    Task<BaseResponse> DeleteUserAsync(); //true with success
    Task<ContentResponse<MultipartFormDataContent>> GetUserAvatarAsync(string id); //null if user not found or user doesn't have avatar
    
}