using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IChatService
{
    Task<ContentResponse<Chat>> GetChatInfoAsync(int id); 
    Task<ContentResponse<List<Chat>>> GetUsersChatsAsync();
    Task<BaseResponse> UploadChatAvatarAsync(int chatId, MultipartFormDataContent file);

}