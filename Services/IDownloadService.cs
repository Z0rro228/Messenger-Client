namespace MessengerApp.Services;
public interface IDownloadService
{
    Task<MultipartFormDataContent?> GetFileAsync(string uri); //Null if file not found
    Task<bool> DeleteFileAsync(string uri); //Delete file from server. Return true if deleting succeeded
}