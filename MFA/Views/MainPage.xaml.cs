namespace MFA.Views;

public partial class MainPage : ContentPage
{
    bool IsAuth;
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    protected override void OnAppearing()
    {
        //if (!IsAuth)
        //    return;
        //Shell.Current.GoToAsync(nameof(LoginPage), true);
        //IsAuth = !IsAuth;
    }
}