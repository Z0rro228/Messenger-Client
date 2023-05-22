using MessengerApp.ViewModels;
namespace MessengerApp.Pages;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
	private async void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
	{
		await (this.BindingContext as ChatPageViewModel).Initialize();
	}
}