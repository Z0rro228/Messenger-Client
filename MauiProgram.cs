using Microsoft.Extensions.Logging;
using MessengerApp.Services;
using MessengerApp.ViewModels;
using MessengerApp.Pages;
using CommunityToolkit.Maui;
namespace MessengerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
			builder.Services.AddSingleton<IInternetProvider, InternetProvider>();
			builder.Services.AddTransient<LoginPageViewModel>();
			builder.Services.AddTransient<LoginPage>();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddTransient<RegistrationPage>();
			builder.Services.AddTransient<RegistrationPageViewModel>();
			builder.Services.AddTransient<ListChatPageViewModel>();
			builder.Services.AddTransient<ChatPageViewModel>();
			builder.Services.AddTransient<ListChatPage>();
			builder.Services.AddTransient<ChatPage>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
