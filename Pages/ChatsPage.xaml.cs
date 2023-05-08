namespace MessengerApp;

public partial class ChatsPage : ContentPage
{
	public ChatsPage()
	{
		InitializeComponent();
	}
	async void OnProfileButtonClicked(object s , EventArgs e)
	{
        await Navigation.PushAsync(new ProfilePage());
    }
    void OnCheckButtonClicked(object s, EventArgs e)
	{
		ChatThanos.IsVisible = true;
	}
}