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
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public string email = "";
        public string username = "";
        public string password = "";
        public string repeatPassword = "";
        public event PropertyChangedEventHandler PropertyChanged;
        public InternetProvider internet_Provider;
        public ICommand RegisterCommand { get; set; }
        public RegistrationViewModel()
        {
            internet_Provider = new InternetProvider();
            RegisterCommand = new Command(() =>
            {
                Login().GetAwaiter();
            });

        }
        async Task Login()
        {
            if (Password == RepeatPassword)
            {
                var response = await internet_Provider.AuthService.RegisterAsync(_Email, Username, Password);
                if (response.StatusCode == 202)
                {
                    Debug.WriteLine("Все верно");
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
                }
                else
                {
                    Debug.WriteLine(response?.StatusMessage);
                    await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("ChatApp", "Указаны разные пароли", "OK");
            }
        }
        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }
        public string RepeatPassword
        {
            get => repeatPassword;
            set
            {
                if (repeatPassword != value)
                {
                    repeatPassword = value;
                    OnPropertyChanged();
                }
            }
        }
        public string _Email
        {
            get => email;
            set
            {
                if (email != value)
                {
                    email = value;
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
