using static System.Net.Mime.MediaTypeNames;

namespace MessengerApp.Models;
public class Message
{
    public string Content{get; set;} = null!;
    public int ChatId{get; set;}
    public int Id{get; set;}
    public DateTime Timestamp { get; set;}
    public string? FromUserId{get; set;}
    public string? FromUserName{get; set;}
    //TODO:public string FromUserAvatar{get; set;}
    public Message(string text)
    {
        Content = text;
    }
    //public string? AttachmentUri { get; set; }
}