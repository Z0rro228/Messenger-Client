using MessengerApp.Services;
using MessengerApp.Services.Responses;
namespace MessengerApp;
public partial class MainPage : ContentPage
{
    string user_name = "";
    string pass_word = "";
    //private IInternetProvider internetProvider;
    public MainPage()
    {
        InitializeComponent();
        //this.internetProvider = internetProvider;
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
       // var response = await authService.LoginAsync(User_name, Pass_word);
  //      if (StatusCode == 200)
        {
            await Navigation.PushAsync(new ChatsPage());
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
}
