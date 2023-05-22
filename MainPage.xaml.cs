using MessengerApp.Services;
namespace MessengerApp;

public partial class MainPage : ContentPage
{
	int count = 0;
	IInternetProvider _internetProvider;

	public MainPage(IInternetProvider internetProvider)
	{
		_internetProvider = internetProvider;
		InitializeComponent();
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}

