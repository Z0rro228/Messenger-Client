using Microsoft.Extensions.Logging;
using MessengerApp.Services;
using MessengerApp.ViewModels;
using MessengerApp.Pages;
namespace MessengerApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
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
			builder.Services.AddSingleton<ListChatPageViewModel>();
			builder.Services.AddSingleton<ChatPageViewModel>();
			builder.Services.AddSingleton<ListChatPage>();
			builder.Services.AddSingleton<ChatPage>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
