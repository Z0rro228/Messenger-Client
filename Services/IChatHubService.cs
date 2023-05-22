using MessengerApp.Models;
using System.Net;
namespace MessengerApp.Services;
public interface IChatHubService : IDisposable
{
    event Action<string>? OnError;
    event Action<Message>? OnRecieveMessage;
    event Action<int, int>? OnDeleteMessage; // Handeler(chatId, messageId)
    event Action<Chat>? OnJoinChat;
    event Action<int>? OnDeleteChat;
    event Action<string, int>? OnLeaveChat;
    event Action<int, int>? OnSetLastReadMessage;
    Task Connect();
    Task Disconnect();
    Task SendMessage(Message msg);
    Task DeleteMessage(int chatId, int messageId);
    Task CreateChat(Chat chat);
    Task DeleteChat(int chatId);
    // Task JoinChat(int chatId);
    Task AddToChat(int chatId, string userId);
    Task LeaveChat(int chatId);
    Task SetLastReadMessage(int chatId, int messageId);
    // bool AuthorizedConnection{get;}
    // void AddCookie(Cookie cookie);
    bool Disposed{get;}
}