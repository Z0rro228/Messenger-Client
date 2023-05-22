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
using System.Collections.ObjectModel;
using MessengerApp.Models;
namespace MessengerApp.ViewModels;
public class ListChatPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private IInternetProvider _internetProvider;
    private User? _userProfile;
    private string _userId = null!;
    private ObservableCollection<Chat> _chats;
    public ListChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        _chats = new ObservableCollection<Chat>();
        OpenChatPageCommand = new Command<int>((param) =>
        {

            Task.Run(async () => 
            {
                IsProcessingNavigate = true;
                await OpenChatPage(param);
            }).GetAwaiter().OnCompleted(() => 
                IsProcessingNavigate = false);
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
    }
    public async Task Refresh()
    {
        var responseOfProfile = await _internetProvider.UserService.GetUserInfoAsync(_userId);
        if(responseOfProfile.StatusCode == 200 || responseOfProfile.StatusCode == 202)
        {
            if(responseOfProfile.Content != null)
                _userProfile = responseOfProfile.Content; 
        }
        else 
        {
            Debug.WriteLine(responseOfProfile.StatusMessage);
            await AppShell.Current.DisplayAlert("ChatApp", responseOfProfile?.StatusMessage, "OK");
            return;
        }
        var responseOfChats = await _internetProvider.ChatService.GetUsersChatsAsync();
        if(responseOfChats.StatusCode == 200 || responseOfChats.StatusCode == 202)
        {
            if(responseOfChats.Content != null)
                _chats = new ObservableCollection<Chat>(responseOfChats.Content); 
        }
        else 
        {
            Debug.WriteLine(responseOfChats?.StatusMessage);
            await AppShell.Current.DisplayAlert("ChatApp", responseOfChats?.StatusMessage, "OK");
            return;
        } 
    }
    async Task OpenChatPage(int chatId)
    {
        await Shell.Current.GoToAsync($"ChatPage?chatId={chatId}");
    }
    private bool isRefreshing;
    public bool IsRefreshing
    {
        get { return isRefreshing; }
        set { isRefreshing = value; OnPropertyChanged(); }
    }
    private bool isProcessingNavigate;
    public bool IsProcessingNavigate
    {
        get { return isProcessingNavigate; }
        set { isProcessingNavigate = value; OnPropertyChanged(); }
    }
    public ICommand OpenChatPageCommand { get; set; }
    public ICommand RefreshCommand {get; set;}
}