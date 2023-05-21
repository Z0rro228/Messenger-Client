using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MessengerApp.Services;
using MessengerApp.Services.Responses;
using MessengerApp.Models;
namespace MessengerApp.ViewModels
{
        public class ChatViewModel : INotifyPropertyChanged
        {
            string content = "";
            string title = "";
            public event PropertyChangedEventHandler PropertyChanged;
            public ICommand AddCommand { get; set; }
            public ICommand AddChatCommand { get; set; }
            public ICommand LogoutCommand { get; set; }
        public ObservableCollection<Message> Messages { get; } = new();
            public ObservableCollection<Chat> Chats { get; } = new();
            public InternetProvider internet_Provider;
        public ChatViewModel()
        {
            Chats = new ObservableCollection<Chat>();
            internet_Provider = new InternetProvider();
            // устанавливаем команду добавления
            AddCommand = new Command(() =>
                {
                    if (Content != "")
                    {
                        Messages.Add(new Message(Content));
                    }
                    Content = "";
                });
            LogoutCommand = new Command(() =>
            {
                Logout().GetAwaiter();
            });
            AddChatCommand = new Command(() =>
            {
                if (Title != "")
                {
                    Chats.Add(new Chat(Title));
                }
                Title = "";
            });
        }
        async Task Logout()
        {
            var response = await internet_Provider.AuthService.LogoutAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());

        }
        public string Content
            {
                get => content;
                set
                {
                    if (content != value)
                    {
                        content = value;
                        OnPropertyChanged();
                    }
                }
            }
        public string Title
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
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            { 

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }
}
