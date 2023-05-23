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
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private int chatId;
    private string title;
    private Chat chatInfo;
    public string _userId;
    public Chat ChatInfo
    {
        get { return chatInfo; }
        set { chatInfo = value; OnPropertyChanged(); }
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;
        chatId = int.Parse(HttpUtility.UrlDecode(query["chatId"].ToString()));
        _userId = HttpUtility.UrlDecode(query["userId"].ToString());
    }

    private IInternetProvider _internetProvider;
    private ObservableCollection<Message> messages = new ObservableCollection<Message>();
    private string message;
    public string Message
    {
        get { return message; }
        set { message = value; OnPropertyChanged(); }
    }
    public ObservableCollection<Message> Messages
    {
        get { return messages; }
        set { messages = value; OnPropertyChanged(); }
    }
    public ChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        SendMessageCommand = new Command(async () =>
        {
            await SendMessage(Message);
        });
        LoadMessagesCommand = new Command(async () =>
        {
            await LoadMessages();
        });
        NavigateToGoBackCommand = new Command(() =>
        {
            if (IsProcessingNavToGoBack) return;
            NavigateToGoBack().GetAwaiter().OnCompleted(() =>
            {
                IsProcessingNavToGoBack = false;
            });
        });
        //TODO connetcing to hub
    }
    private async Task LoadMessages()
    {
        var messagesResponse = await _internetProvider.MessagesService.GetAllMessages(chatId);
        if (messagesResponse.StatusCode == 200 || messagesResponse.StatusCode == 202)
        {
            Messages = new ObservableCollection<Message>(messagesResponse.Content!);
        }
        else
        {
            await AppShell.Current.DisplayAlert("ChatApp", messagesResponse?.StatusMessage, "OK");
        }
    }
    private async Task SendMessage(string content, string? attach = null)
    {
        // await _internetProvider.ChatHubService.SendMessage(new Message(){Content = content});
        await AppShell.Current.DisplayAlert("ChatApp", content, "OK");
    }
    public ICommand SendMessageCommand { get; set; }
    public ICommand LoadMessagesCommand { get; set; }
    public ICommand NavigateToGoBackCommand { get; set; }
    public async Task Initialize()
    {
        var chatInfoResponse = await _internetProvider.ChatService.GetChatInfoAsync(chatId);
        if (chatInfoResponse.StatusCode == 200 || chatInfoResponse.StatusCode == 202)
        {
            chatInfo = chatInfoResponse.Content!;
            _Title = chatInfo.Title;
        }
        else
        {
            await AppShell.Current.DisplayAlert("ChatApp", chatInfoResponse?.StatusMessage, "OK");
            return;
        }
        await LoadMessages();
    }
    async Task NavigateToGoBack()
    {
        await Shell.Current.GoToAsync($"ListChatPage?userId={_userId}");
    }
    private bool isProcessingNavToGoBack;
    public bool IsProcessingNavToGoBack
    {
        get { return isProcessingNavToGoBack; }
        set { isProcessingNavToGoBack = value; OnPropertyChanged(); }
    }

    public string _Title
    {
        get => title;
        set
        {
            if (title != value)
            {
                title = value;
                OnPropertyChanged();
            }
        }
    }
}
