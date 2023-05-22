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
			builder.Services.AddSingleton<LoginPageViewModel>();
			builder.Services.AddSingleton<LoginPage>();
			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<RegistrationPage>();
			builder.Services.AddSingleton<RegistrationPageViewModel>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
