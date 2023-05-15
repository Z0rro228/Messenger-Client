using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IDownloadService
{
    Task<ContentResponse<MultipartFormDataContent>> GetFileAsync(string uri); //Null if file not found
    Task<BaseResponse> DeleteFileAsync(string uri); //Delete file from server. Return true if deleting succeeded
}