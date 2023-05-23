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
using MessengerApp.Pages;

namespace MessengerApp.ViewModels;
public class RegistrationPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    private IInternetProvider _internetProvider;
    public ICommand RegisterCommand { get; set; }
    private string email = "";
    private string userName = "";
    private string password = "";
    private string repeatedPassword = "";
    private bool isProcessing;
    public bool IsProcessing
    {
        get { return isProcessing; }
        set { isProcessing = value; OnPropertyChanged(); }
    }
    public RegistrationPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        RegisterCommand = new Command(() =>
        {
            if (IsProcessing) return;
            if (Password != RepeatedPassword || Password.Trim() == "" ||
                UserName.Trim() == "" || Email.Trim() == "")
            {
                AppShell.Current.DisplayAlert("ChatApp","Ќекоторые пол€ пустые или пароли не совпадают", "OK");
                return;
            }
  
            IsProcessing = true;
            Register().GetAwaiter().OnCompleted(() =>
            {
                IsProcessing = false;
            });
        });
    }
    
    public string UserName
    {
        get { return userName; }
        set { userName = value; OnPropertyChanged(); }
    }

    public string Password
    {
        get { return password; }
        set { password = value; OnPropertyChanged(); }
    }
    public string Email
    {
        get { return email; }
        set { email = value; OnPropertyChanged(); }
    }

    public string RepeatedPassword
    {
        get { return repeatedPassword; }
        set { repeatedPassword = value; OnPropertyChanged(); }
    }
    async Task Register()
    {
        var response = await _internetProvider.AuthService.RegisterAsync(Email, UserName, Password);
        // if (response.StatusCode == 200 || response.StatusCode == 202)
        // {
        //     Debug.WriteLine("Good response of registration");
        //     // await Shell.Current.GoToAsync($"ListChatPage?userId={response.Id}");
        //     // await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
        // }
        // else
        // {
        //     Debug.WriteLine(response?.StatusMessage);
        //     await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
        //     await Shell.Current.GoToAsync(nameof(MainPage), true);
        // }
        await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
        // await Shell.Current.GoToAsync("LoginPage", true);
    }
}