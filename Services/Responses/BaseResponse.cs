namespace MessengerApp.Services.Responses;
public class BaseResponse
{
    public int StatusCode { get; set; }
    public string? StatusMessage { get; set; }
}