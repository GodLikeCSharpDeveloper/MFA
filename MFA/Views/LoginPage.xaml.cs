using MFA.Services;
using MFA.Services.DBService;
namespace MFA.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}