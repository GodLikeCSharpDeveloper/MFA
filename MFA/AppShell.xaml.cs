﻿using MFA.Services.DBService;
using MFA.Views;

namespace MFA;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DetailsTopicPage), typeof(DetailsTopicPage)); 
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
        Routing.RegisterRoute(nameof(TopicAddOrRemove), typeof(TopicAddOrRemove));
    }

    private async void LogOut(object sender, EventArgs e)
    {
        await RealmService.app.CurrentUser.LogOutAsync();
        await Shell.Current.GoToAsync("LoginPage", true);
    }
}
