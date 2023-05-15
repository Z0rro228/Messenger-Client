namespace MessengerApp.Services.Responses;
public class ContentResponse<T>: BaseResponse
{
    public T? Content{get; set;}
}