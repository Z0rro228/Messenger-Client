using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using MessengerApp.Models;
using System.Net;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Web;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Logging;
namespace MessengerApp.Services;

public class ChatHubService : IChatHubService //TODO: TEST FOR IDISPOSABLE
{
    public readonly HubConnection hubConnection;
    private HttpClient httpClient;
    private string _serverRootUrl;
    public event Action<string>? OnError;
    public event Action<Message>? OnRecieveMessage;
    public event Action<int, int>? OnDeleteMessage; // Handler(chatId, messageId)
    public event Action<Chat>? OnJoinChat;
    public event Action<int>? OnDeleteChat;
    public event Action<string, int>? OnLeaveChat; //Handler(userId, chatId)
    public event Action<int, int>? OnSetLastReadMessage; //Handler(chatId, msgId)
    public ChatHubService(HttpClient httpClient, string url, CookieContainer authCookies)
    {
        _serverRootUrl = url;
        this.httpClient = httpClient;
        hubConnection = new HubConnectionBuilder()
        .WithUrl($"{_serverRootUrl}/chat", options => 
        {
            options.Cookies = authCookies;
        })
        .Build();
        
        hubConnection.On<Chat>("OnJoinChat", chat => {
            OnJoinChat?.Invoke(chat);
        });
        hubConnection.On<string>("OnError", str => {
            OnError?.Invoke(str);
        });
        hubConnection.On<Message>("OnRecieveMessage", msgObj => 
        {
            OnRecieveMessage?.Invoke(msgObj);
        });
        hubConnection.On<int, int>("OnDeleteMessage", (chatId, msgId) => {
            OnDeleteMessage?.Invoke(chatId, msgId);
        });
        hubConnection.On<int>("OnDeleteChat", chatId =>{
            OnDeleteChat?.Invoke(chatId);
        });
        hubConnection.On<string, int>("OnLeaveChat", (userId, chatId) => 
        {
            OnLeaveChat?.Invoke(userId, chatId);
        });
        hubConnection.On<int, int>("OnSetLastReadMessage", (chatId, msgid) => 
        {
            OnSetLastReadMessage?.Invoke(chatId, msgid);
        });
    }
    public async Task Connect()
    {
        // hubConnection
        if (hubConnection.State == HubConnectionState.Disconnected)
            await hubConnection.StartAsync();
    }
    public async Task Disconnect()
    {
        await hubConnection.StopAsync();
    }
    public async Task SendMessage(Message message)
    {
        await hubConnection.InvokeAsync("SendMessage", message);
    }
    public async Task DeleteMessage(int chatId, int messageId)
    {
        await hubConnection.InvokeAsync("DeleteMessage", chatId, messageId);
    }
    public async Task CreateChat(Chat chat)
    {
        await hubConnection.InvokeAsync("CreateChat", chat);
    } 
    public async Task DeleteChat(int chatId)
    {
        await hubConnection.InvokeAsync("DeleteChat", chatId);
    }
    // public async Task JoinChat(int chatId)
    // {
    //     // await 
    //     throw new NotImplementedException();
    // }
    public async Task AddToChat(int chatId, string userId)
    {
        await hubConnection.InvokeAsync("JoinChat", chatId, userId);
    }
    public async Task LeaveChat(int chatId)
    {
        await hubConnection.InvokeAsync("LeaveChat", chatId);
    }   
    public async Task SetLastReadMessage(int chatId, int messageId)
    {
        await hubConnection.InvokeAsync("SetLastReadMessage", chatId, messageId);
    }
    private bool disposed = false;
    public bool Disposed 
    {
        get => disposed;
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
 
    protected virtual async void Dispose(bool disposing)
    {
        if (disposed) return;
        if (disposing)
        {
            await Disconnect();
        }
        disposed = true;
    }
    // public bool Authorized {get; private set;}
    // public void AddCookie(Cookie cookie)
    // {
    //     Authorized = true;
    // }
    ~ChatHubService()
    {
        Dispose (false);
    }
}