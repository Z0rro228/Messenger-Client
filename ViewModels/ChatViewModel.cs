   using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using System.Collections.ObjectModel;
    using System.Xml.Linq;
    using MessengerApp.Models;
using MessengerApp.Services;
using MessengerApp.Services.Responses;
namespace MessengerApp.ViewModels
{
        public class ChatViewModel : INotifyPropertyChanged
        {
            string content = "";
            string title = "";
            public event PropertyChangedEventHandler PropertyChanged;
            public ICommand AddCommand { get; set; }
            public ICommand AddChatCommand { get; set; }
            public ObservableCollection<Message> Messages { get; } = new();
            public ObservableCollection<Chat> Chats { get; } = new();

            public ChatViewModel()
            {
                
            
                // устанавливаем команду добавления
                AddCommand = new Command(() =>
                {
                    if (Content != "")
                    {
                        Messages.Add(new Message(Content));
                    }
                    Content = "";
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
