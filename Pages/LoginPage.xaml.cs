using MessengerApp.Services;
using MessengerApp.Services.Responses;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading;

namespace MessengerApp;
public partial class LoginPage : ContentPage
{ 
    public LoginPage()
    {
        InitializeComponent();
    }
    async void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegistrationPage());
    }
    void OnShowRegisterPasswordButtonClicked(object sender, EventArgs e)
    {
        if (RegistrationPassword.IsPassword)
        {
            ShowRegisterPasswordButton.Source = "eye_closed.png";
            RegistrationPassword.IsPassword = false;
        }
        else
        {
            ShowRegisterPasswordButton.Source = "eye_open.png";
            RegistrationPassword.IsPassword = true;
        }
    }
}
