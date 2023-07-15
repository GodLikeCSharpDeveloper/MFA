using MFA.Services;
using MFA.Services.LikeRepository;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.NotificationService;
using MFA.Services.RegisterServices;
using MFA.Services.TopicService;
using MFA.Services.UsersCommentsService;
using MFA.Services.UserService;
using MFA.Services.ValidateService;
using MFA.Utility.ImageManager;
using MFA.Utility.UiHelper;
using MFA.Utility.UiHelper.CollectionUiLogic;
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
                fonts.AddFont("CormorantGaramond-Regular.ttf", "CormorantRegular");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();

        builder.Services.AddSingleton<RegisterPage>();
		builder.Services.AddSingleton<RegisterPageViewModel>();

        builder.Services.AddSingleton<DetailsTopicPage>();
        builder.Services.AddSingleton<TopicDetailViewModel>();

        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<LoginPageViewModel>();

        builder.Services.AddSingleton<UserInfoPage>();
        builder.Services.AddSingleton<UserInfoViewModel>();

        builder.Services.AddSingleton<TopicAddOrRemove>();
        builder.Services.AddSingleton<TopicAddOrRemoveViewModel>();

        builder.Services.AddSingleton<UserEditPage>();
        builder.Services.AddScoped<UserEditViewModel>();

        builder.Services.AddSingleton<IRegisterRepository, RegisterRepository>();
        builder.Services.AddSingleton<IRegisterManager, RegisterManager>();
        builder.Services.AddSingleton<IRegisterRepository, RegisterRepository>();

        builder.Services.AddSingleton<ILoginRepos, LoginRepository>();
        builder.Services.AddSingleton<ILoginManager, LoginManager>();
        builder.Services.AddSingleton<IUserLogOut, UserLogOut>();

        builder.Services.AddSingleton<IUserValidator, UserValidator>();

        builder.Services.AddSingleton<INavigationRepository, NavigationRepository>();

        builder.Services.AddSingleton<INotificationService, NotificationService>();

        builder.Services.AddScoped<ITopicDBService, TopicDbService>();

        builder.Services.AddSingleton<IUsersCommentService, UsersCommentService>();

        builder.Services.AddSingleton<ILikeRepository, LikeRepository>();

        builder.Services.AddScoped<ICollectionUiLogic<Topic>, CollectionUiLogic<Topic>>();

        builder.Services.AddScoped<ICollectionUiLogic<UsersComment>, CollectionUiLogic<UsersComment>>();

        builder.Services.AddScoped<IDbChecker, DbChecker>();

        builder.Services.AddSingleton<IUserDbService, UserDbService>();

        builder.Services.AddScoped<IImageManager, ImageManager>();

        builder.Services.AddSingleton<TopicServices>();

       
        return builder.Build();
    }
}
