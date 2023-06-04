using MFA.Services;
using MFA.ViewModels;
using MFA.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

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
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton(new MongoClient("mongodb://localhost:27017"));
        return builder.Build();
    }
}
