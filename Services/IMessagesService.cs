using MessengerApp.Models;
using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IMessagesService
{
    Task<ContentResponse<List<Message>>> GetMessagesRangeAsync(int chatId, int fromMsgId, int range);
    Task<ContentResponse<int>> GetLastReadMessageIdAsync(int chatId); 
    Task<ContentResponse<int>> GetNewestMessageIdAsync(int chatId);
    Task<BaseResponse> UploadFileAsync(Stream file, FileResult res);
    Task<ContentResponse<List<Message>>> GetAllMessages(int chatId); //bad practice
}