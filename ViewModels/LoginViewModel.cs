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
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string user_name = "";
        public string pass_word = "";
        public event PropertyChangedEventHandler PropertyChanged;
        public InternetProvider internet_Provider;
        public ICommand LoginCommand { get; set; }
        public LoginViewModel()
        {
            internet_Provider = new InternetProvider();
            LoginCommand = new Command(() =>
            {
                Login().GetAwaiter();
            });

        }
        async Task Login()
        {
            var response = await internet_Provider.AuthService.LoginAsync(User_name, Pass_word);
            if (response.StatusCode == 200)
            {
                Debug.WriteLine("Все верно");
                await Application.Current.MainPage.Navigation.PushAsync(new ChatsPage());
            }
            else
            {
                Debug.WriteLine(response?.StatusMessage);
                await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
            }
        }
        public string User_name
        {
            get => user_name;
            set
            {
                if (user_name != value)
                {
                    user_name = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Pass_word
        {
            get => pass_word;
            set
            {
                if (pass_word != value)
                {
                    pass_word = value;
                    OnPropertyChanged();
                }
            }
        }
        void OnPropertyChanged([CallerMemberName] string prop = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
