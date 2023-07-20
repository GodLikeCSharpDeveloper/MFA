using MFA.Services.NavigationService;
using MFA.ViewModels;

namespace MFA.Views;

public partial class UserEditPage : ContentPage
{

    public UserEditPage(UserEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}