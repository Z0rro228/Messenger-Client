using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IMessagesService
{
    Task<ContentResponse<IEnumerable<Message>>> GetMessagesRangeAsync(int chatId, int fromMsgId, int range);
    Task<ContentResponse<int>> GetLastReadMessageIdAsync(int chatId);
    Task<ContentResponse<int>> GetNewestMessageIdAsync(int chatId); 
    Task<ContentResponse<string>> UploadFileAsync(MultipartFormDataContent file); 
}