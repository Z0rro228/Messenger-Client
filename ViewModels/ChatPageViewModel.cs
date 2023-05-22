using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MessengerApp.Services;
using MessengerApp.Services.Responses;
using MessengerApp;
using System.Windows.Input;
using System.Diagnostics;
using System.Web;
using MessengerApp.Models;
using System.Collections.ObjectModel;
namespace MessengerApp.ViewModels;
public class ChatPageViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private int chatId;
    private Chat? chatInfo;
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;
        chatId = int.Parse(HttpUtility.UrlDecode(query["chatId"].ToString()));            
    }
    private IInternetProvider _internetProvider;
    private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
    private int lastReadMessageId;
    private int currentMessageId;
    private bool scrollingDown;
    public int LastReadMessageId
    {
        get{return lastReadMessageId;}
        set{
            lastReadMessageId = value; 
            OnPropertyChanged();
            new Task(async () =>
                {
                    await SetLastReadMessage();
                }).Start();        
            }
    }
    public int CurentMessageId
    {
        get{return currentMessageId;}
        set{
            currentMessageId = value; 
            if(currentMessageId > LastReadMessageId)
                LastReadMessageId = currentMessageId;
            OnPropertyChanged(); }
    }
    public bool ScrollingDown
    {
        get{return scrollingDown;}
        set{
            scrollingDown = value;
            OnPropertyChanged();
        }
    }
    public ChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        SendMessageCommand = new Command<string>(async (param) => 
        {
            await SendMessage(param);
        });
        LoadMessagesCommand = new Command(async () => {
            //TODO: TEST IT
            await LoadMessages(currentMessageId);
        });
        //TODO connetcing to hub

    }
    private async Task LoadMessages(int fromId)
    {
        var messagesResponse = await _internetProvider.MessagesService.GetMessagesRangeAsync(chatId, fromId, 
                (scrollingDown) ? 30 : -30);
        if(messagesResponse.StatusCode == 200 || messagesResponse.StatusCode == 202)
        {
            if(ScrollingDown)
                foreach(var m in messagesResponse.Content!)
                {
                    _messages.Add(m);
                }
            else 
            {
                messagesResponse.Content!.Reverse();
                foreach(var m in messagesResponse.Content!)
                {
                    _messages.Insert(0, m);
                }
            }
        }
        else
        {
            await AppShell.Current.DisplayAlert("ChatApp", messagesResponse?.StatusMessage, "OK");
        }
    }
    private async Task SendMessage(string content, string? attach = null)
    {
        throw new NotImplementedException();
        await _internetProvider.ChatHubService.SendMessage(new Message(content));
    }
    private async Task SetLastReadMessage()
    {
        await _internetProvider.ChatHubService.SetLastReadMessage(chatId, LastReadMessageId);
    }
    public ICommand SendMessageCommand {get; set;}
    public ICommand LoadMessagesCommand {get; set;}
    public async Task Initialize()
    {
        ScrollingDown = true;
        var chatInfoResponse = await _internetProvider.ChatService.GetChatInfoAsync(chatId);
        if(chatInfoResponse.StatusCode == 200 || chatInfoResponse.StatusCode == 202)
        {
            chatInfo = chatInfoResponse.Content!;
        }
        else 
        {
            await AppShell.Current.DisplayAlert("ChatApp", chatInfoResponse?.StatusMessage, "OK");
            return;
        }
        var lastMsgResponse = await _internetProvider.MessagesService
            .GetLastReadMessageIdAsync(chatId);
        if(lastMsgResponse.StatusCode == 200 || lastMsgResponse.StatusCode == 202)
        {
            LastReadMessageId = lastMsgResponse.Content!;
        }
        else 
        {
            await AppShell.Current.DisplayAlert("ChatApp", lastMsgResponse?.StatusMessage, "OK");
            return;
        }
        await LoadMessages(lastReadMessageId);
    }
}