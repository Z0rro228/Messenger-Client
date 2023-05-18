using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IUserService
{
    Task<ContentResponse<User>> GetUserInfoAsync(string id);
    Task<ContentResponse<string>> SetAvatarOfUserAsync(MultipartFormDataContent file); 
    Task<BaseResponse> DeleteUserAsync();
    Task<ContentResponse<MultipartFormDataContent>> GetUserAvatarAsync(string id); 
    
}