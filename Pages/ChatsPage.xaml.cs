namespace MauiApp3;

public partial class ChatsPage : ContentPage
{
	public ChatsPage()
	{
		InitializeComponent();
	}
	void OnProfileButtonClicked(object s , EventArgs e)
	{
		qwerty.Text = "Profile CLicked";
	}
    void OnCheckButtonClicked(object s, EventArgs e)
	{
		ChatThanos.IsVisible = true;
	}
}