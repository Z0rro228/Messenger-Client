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
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private IInternetProvider _internetProvider;
    private User _userProfile;
    private string _userId;
    private ObservableCollection<Chat> _chats;
    public ListChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        //TODO

    }
    async Task Refresh()
    {
        var responseOfProfile = await _internetProvider.UserService.GetUserInfoAsync(_userId);
        if(responseOfProfile.StatusCode == 200 || responseOfProfile.StatusCode == 202)
        {
            if(responseOfProfile.Content != null)
                _userProfile = responseOfProfile.Content; 
        }
        else 
        {
            //TODO
        }
        var responseOfChats = await _internetProvider.ChatService.GetUsersChatsAsync();
        if(responseOfChats.StatusCode == 200 || responseOfChats.StatusCode == 202)
        {
            if(responseOfChats.Content != null)
                _chats = new ObservableCollection<Chat>(responseOfChats.Content); 
        }
        else 
        {
            //TODO
        }
        
    }
    public ICommand OpenChatPageCommand { get; set; }
    public ICommand RefreshCommand {get; set;}
}