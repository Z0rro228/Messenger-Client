using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IChatService
{
    Task<ContentResponse<Chat>> GetChatInfoAsync(int id); // null if chat not found
    Task<ContentResponse<IEnumerable<Chat>>> GetUsersChatsAsync(); //Return all chats with current user
    Task<ContentResponse<string>> UploadChatAvatarAsync(MultipartFormDataContent file); //Return URI to download file from server
}