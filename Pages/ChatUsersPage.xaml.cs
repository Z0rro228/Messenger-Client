namespace MessengerApp.Pages;

public partial class ChatUsersPage : ContentPage
{
	public ChatUsersPage()
	{
		InitializeComponent();
	}
    async void OnBackToChatButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}