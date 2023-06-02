using MFA.Services;
using MFA.ViewModels;
using MFA.Views;
using Microsoft.Extensions.Logging;

namespace MFA;

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

#if DEBUG
		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<TopicService>();
        builder.Services.AddTransient<DetailsTopicPage>();
		builder.Services.AddTransient<TopicDetailViewModel>();
		return builder.Build();
	}
}
