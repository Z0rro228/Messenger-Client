using Microsoft.Extensions.Logging;

namespace MessengerApp;

public partial class RegistrationPage : ContentPage
{
    public RegistrationPage()
    {
        InitializeComponent();

    }
    async void OnAddUserButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
        string text = ((Entry)PasswordEntry).Text;
    }
    async void OnBackToAuthorizationButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
    void OnShowAuthorizationPasswordButtonClicked(object sender, EventArgs e)
    {
        if (PasswordEntry.IsPassword)
        {
            ShowAuthorizationPasswordButton.Source = "eye_closed.png";
            PasswordEntry.IsPassword = false;
        }
        else
        {
            ShowAuthorizationPasswordButton.Source = "eye_open.png";
            PasswordEntry.IsPassword = true;
        }
    }
}