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
    public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query == null || query.Count == 0) return;
            chatId = int.Parse(HttpUtility.UrlDecode(query["chatId"].ToString()));            
        }
    private IInternetProvider _internetProvider;
    private ObservableCollection<Message> _messages;
    public ChatPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        //TODO

    }
}