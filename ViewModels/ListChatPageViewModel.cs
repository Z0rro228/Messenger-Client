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
using System.Web;
using System.Diagnostics;
using System.Collections.ObjectModel;
using MessengerApp.Models;
using System;

namespace MessengerApp.ViewModels;
public class ListChatPageViewModel : INotifyPropertyChanged, IQueryAttributable
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query == null || query.Count == 0) return;
        _userId = HttpUtility.UrlDecode(query["userId"].ToString())!;            
    }
    public IInternetProvider _internetProvider;
    public User? _userProfile;
    public string _userId;
    public ObservableCollection<Chat> Chats
    {
        get { return chats; }
        set { chats = value; OnPropertyChanged(); }
    }

    private ObservableCollection<Chat> chats;
    private string newChatTitle;
    private bool isProcessingAdd;
    public bool IsProcessingAdd
    {
        get {return isProcessingAdd;}
        set {isProcessingAdd = value; OnPropertyChanged();}
    }
    public string NewChatTitle
    {
        get {return newChatTitle;}
        set {newChatTitle = value; OnPropertyChanged();}
    }
    private bool isConnected;
    public bool IsConnected
    {
        get => isConnected;
        set
        {
            if (isConnected != value)
            {
                isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }
    }
    public ListChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        _internetProvider.ChatHubService.Connect();
        Chats = new ObservableCollection<Chat>();
        OpenChatPageCommand = new Command<int>((param) =>
        {
            if(IsProcessingNavigate) return;
            OpenChatPage(param).GetAwaiter().OnCompleted(() => 
            {
                IsProcessingNavigate = false;
            });

        });
        RefreshCommand = new Command(() => 
        {
            Task.Run(async () =>
            {
                IsRefreshing = true;
                await Refresh();
            }).GetAwaiter().OnCompleted(() =>
            {
                IsRefreshing = false;
            });
        });
        
        AddChatCommand = new Command<bool>((isGroup) => 
        {
            if(NewChatTitle == null) return;
            if(NewChatTitle.Trim() == "") return;
            if(IsProcessingAdd == false)
                AddChat(NewChatTitle, isGroup).GetAwaiter().OnCompleted(() => {IsProcessingAdd = false;});
        });
        OnImageBtnClicked = new Command(async () => 
        {
            await SetAvatar();
        });
    }
    public async Task Refresh()
    {
        var responseOfProfile = await _internetProvider.UserService.GetUserInfoAsyncById(_userId);
        if (responseOfProfile.StatusCode == 200 || responseOfProfile.StatusCode == 202)
        {
            if(responseOfProfile.Content != null)
                _userProfile = responseOfProfile.Content; 
                if(_userProfile.Avatar != null) Avatar = _userProfile.Avatar;
        }
        else 
        {
            Debug.WriteLine(responseOfProfile.StatusMessage);
            await AppShell.Current.DisplayAlert("ChatApp", responseOfProfile?.StatusMessage, "OK");
            return;
        }
        var responseOfChats = await _internetProvider.ChatService.GetUsersChatsAsync();
        if (responseOfChats.StatusCode == 200 || responseOfChats.StatusCode == 202)
        {
            if(responseOfChats.Content != null)
                Chats = new ObservableCollection<Chat>(responseOfChats.Content);
        }
        else 
        {
            Debug.WriteLine(responseOfChats?.StatusMessage);
            await AppShell.Current.DisplayAlert("ChatApp", responseOfChats?.StatusMessage, "OK");
            return;
        } 
    }
    async Task AddChat(string Title, bool isGroup)
    {
        var chat = new Chat()
        {
            Title = Title,
            AdminId = _userId,
            UsersId = new List<string>()
            {
                _userId
            },
            IsGroup = isGroup
        };
        await _internetProvider.ChatHubService.CreateChat(chat);
    }
    async Task OpenChatPage(int chatId)
    {
        await Shell.Current.GoToAsync($"ChatPage?chatId={chatId}&userId={_userId}");
    }
    public bool isRefreshing;
    public bool IsRefreshing
    {
        get { return isRefreshing; }
        set { isRefreshing = value; OnPropertyChanged(); }
    }
    public bool isProcessingNavigate;
    public bool IsProcessingNavigate
    {
        get { return isProcessingNavigate; }
        set { isProcessingNavigate = value; OnPropertyChanged(); }
    }
    public ICommand OpenChatPageCommand { get; set; }
    public ICommand RefreshCommand {get; set;}
    public ICommand AddChatCommand{get; set;}
    private bool isProcessingNavToAdd;
    public bool IsProcessingNavToAdd
    {
        get { return isProcessingNavToAdd; }
        set { isProcessingNavToAdd = value; OnPropertyChanged(); }
    }
    public string Avatar
    {
        get {
            if(_userProfile?.Avatar == null)
            return "dotnet_bot.svg";
            else return _userProfile.Avatar;
        }
        set 
        {
            _userProfile.Avatar = value; OnPropertyChanged();
        }
    }
    public ICommand OnImageBtnClicked{get; set;}
    private async Task SetAvatar()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle="Select file",
            FileTypes=FilePickerFileType.Images
        });
        if(result == null) return;
        var stream = await result.OpenReadAsync();
        var resp = await _internetProvider.UserService.SetAvatarOfUserAsync(stream);
        if(resp.StatusCode == 200 || resp.StatusCode == 202)
        {
            Avatar = resp.StatusMessage;
            await AppShell.Current.DisplayAlert("ChatApp", Avatar, "OK");

        }
        else 
        {
            await AppShell.Current.DisplayAlert("ChatApp", resp?.StatusMessage, "OK");
        }
    }
}
