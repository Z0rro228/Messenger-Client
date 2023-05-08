namespace MessengerApp;
public partial class MainPage : ContentPage
{
    public MainPage()
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
    async void OnLogInButtonClicked(object sender, EventArgs e)
    {
        
        await Navigation.PushAsync(new ChatsPage());
    }
}
