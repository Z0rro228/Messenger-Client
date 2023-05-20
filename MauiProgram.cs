using Microsoft.Extensions.Logging;
using MessengerApp.Services;
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
		//builder.Services.AddSingleton<InternetProvider>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
