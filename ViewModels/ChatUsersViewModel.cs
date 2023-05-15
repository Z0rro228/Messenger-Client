using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using MessengerApp.Models;

namespace MessengerApp.ViewModels
{ 
    public class ChatUsersViewModel : BindableObject
    {
        public ObservableCollection<User> _ChatUsers;
        public ObservableCollection<User> ChatUsers
        {
            get => _ChatUsers;
            set
            {
                _ChatUsers = value;
                OnPropertyChanged();
            }
        }

        public ChatUsersViewModel()
        {
            // Инициализируем список кнопок
            ChatUsers = new ObservableCollection<User>
        {
            new User {UserName="User 1" },
            new User {UserName="User 2" },
            new User {UserName="User 3" },
        };

        }
    }
}


