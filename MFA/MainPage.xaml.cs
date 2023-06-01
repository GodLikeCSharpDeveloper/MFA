using MFA.ViewModels;

namespace MFA;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
        InitializeComponent();
    }
}

