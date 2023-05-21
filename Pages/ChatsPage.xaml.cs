using MessengerApp.Pages;
using MessengerApp.ViewModels;
namespace MessengerApp;

public partial class ChatsPage : ContentPage
{
    public ChatsPage()
    {
        InitializeComponent();
        BindingContext = new ChatViewModel();
        this.MinimumHeightRequest = 600;
        this.MinimumWidthRequest = 700;
    }

    async void OnProfileButtonClicked(object s, EventArgs e)
    {
        await Navigation.PushAsync(new ProfilePage());
    }

    void OnCheckButtonClicked(object s, EventArgs e)
    {
        ChatThanos.IsVisible = true;
        HeaderThanos.IsVisible = true;
    }
    async void OnChatUsersButtonClicked(object s, EventArgs e)
    {
        await Navigation.PushModalAsync(new ChatUsersPage());
    }
}