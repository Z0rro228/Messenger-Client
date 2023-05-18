using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IChatService
{
    Task<ContentResponse<Chat>> GetChatInfoAsync(int id);
    Task<ContentResponse<IEnumerable<Chat>>> GetUsersChatsAsync();
    Task<ContentResponse<string>> UploadChatAvatarAsync(MultipartFormDataContent file);
}