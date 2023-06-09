﻿using MFA.Services;
using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;
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
        builder.Services.AddSingleton<RegisterPage>();
		builder.Services.AddSingleton<RegisterPageViewModel>();
        builder.Services.AddSingleton<TopicService>();
        builder.Services.AddTransient<DetailsTopicPage>();
		builder.Services.AddTransient<TopicDetailViewModel>();
		builder.Services.AddSingleton<IRegister, RealmRegisterRepos>();
		builder.Services.AddSingleton<ILoginRepos, RealmLoginRepository>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<IUserValidator, UserValidator>();
        builder.Services.AddTransient<LoginPageViewModel>();
		builder.Services.AddSingleton<UserRepository>();
        builder.Services.AddSingleton<TopicAddOrRemove>();
        builder.Services.AddTransient<TopicAddOrRemoveViewModel>();
		builder.Services.AddScoped<ITopicDBService, TopicDbService>();
        builder.Services.AddScoped<IRegister, RealmRegisterRepos>();
        builder.Services.AddSingleton(new MongoClient("mongodb://localhost:27017"));
        return builder.Build();
    }
}
