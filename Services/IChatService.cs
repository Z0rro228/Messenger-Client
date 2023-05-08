using MessengerApp.Models;
namespace MessengerApp.Services;
public interface IChatService
{
    Task<Chat?> GetChatInfoAsync(int id); // null if chat not found
    Task<IEnumerable<Chat>> GetUsersChatsAsync(); //Return all chats with current user
    Task<string?> UploadChatAvatarAsync(MultipartFormDataContent file); //Return URI to download file from server
}