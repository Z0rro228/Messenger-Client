using MessengerApp.Models;
namespace MessengerApp.Services;
public interface IMessagesService
{
    //Get range of messages. Beginning from messageid and with count range. If range<0 then messages download in reverse
    Task<IEnumerable<Message>> GetMessagesRangeAsync(int chatId, int fromMsgId, int range);
    Task<int?> GetLastReadMessageIdAsync(int chatId); //Return id of last read message in this chat.
    Task<int?> GetNewestMessageIdAsync(int chatId); //Return id of newest message in this chat
    Task<string?> UploadFileAsync(MultipartFormDataContent file); //Return URI of file to download
}