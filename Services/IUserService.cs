using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IUserService
{
    Task<ContentResponse<User>> GetUserInfoAsyncById(string id);
    Task<ContentResponse<User>> GetUserInfoAsyncByName(string name);
    Task<BaseResponse> SetAvatarOfUserAsync(Stream file);
    Task<BaseResponse> DeleteUserAsync();    
}