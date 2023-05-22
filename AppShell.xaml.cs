using MessengerApp.Pages;
namespace MessengerApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
		// Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
		Routing.RegisterRoute(nameof(ListChatPage), typeof(ListChatPage));
		Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
	}
}
