using MessengerApp.ViewModels;
namespace MessengerApp.Pages;

public partial class ChatPage : ContentPage
{
	public ChatPage(ChatPageViewModel viewModel)
	{
		InitializeComponent();

		this.BindingContext = viewModel;
	}
}