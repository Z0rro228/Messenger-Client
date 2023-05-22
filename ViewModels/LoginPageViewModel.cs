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
public class LoginPageViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private IInternetProvider _internetProvider;

    public LoginPageViewModel(IInternetProvider internetProvider)
    {
        _internetProvider = internetProvider;
        LoginCommand = new Command(() =>
        {
            if (IsProcessingLogin) return;

            if (UserName.Trim() == "" || Password.Trim() == "") return;

            IsProcessingLogin = true;
            Login().GetAwaiter().OnCompleted(() =>
            {
                IsProcessingLogin = false;
            });
        });
        NavigateToRegisterCommand = new Command(() => 
        {
            if(isProcessingNavToReg) return;
            NavigateToRegister().GetAwaiter().OnCompleted(() => 
            {
                IsProcessingNavToReg = false;
            });
        });
    }

    async Task Login()
    {
        var response = await _internetProvider.AuthService.LoginAsync(UserName, Password);
        if (response.StatusCode == 200)
        {
            Debug.WriteLine("Good response of authorization");
            await Shell.Current.GoToAsync(nameof(ListChatPage), true);
            // await Shell.Current.GoToAsync($"ListChatPage?userId={response.Id}");
            // await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
        }
        else
        {
            Debug.WriteLine(response?.StatusMessage);
            await AppShell.Current.DisplayAlert("ChatApp", response.StatusMessage, "OK");
            // await Shell.Current.GoToAsync(nameof(MainPage), true);
            await Shell.Current.GoToAsync(nameof(ListChatPage), true);
        }
    }
    async Task NavigateToRegister()
    {
        await Shell.Current.GoToAsync("RegistrationPage");
    }

    private string userName = "";
    private string password = "";
    private bool isProcessingLogin;
    private bool isProcessingNavToReg;

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

    public bool IsProcessingLogin
    {
        get { return isProcessingLogin; }
        set { isProcessingLogin = value; OnPropertyChanged(); }
    }
    public bool IsProcessingNavToReg
    {
        get { return isProcessingNavToReg; }
        set { isProcessingNavToReg = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; set; }
    public ICommand NavigateToRegisterCommand{get; set;}
}
