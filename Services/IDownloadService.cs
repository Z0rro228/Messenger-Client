using MessengerApp.Services.Responses;
namespace MessengerApp.Services;
public interface IDownloadService
{
    Task<ContentResponse<MultipartFormDataContent>> GetFileAsync(string uri);
    Task<BaseResponse> DeleteFileAsync(string uri);
}