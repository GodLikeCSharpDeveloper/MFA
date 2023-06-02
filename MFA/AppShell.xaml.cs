using MFA.Views;

namespace MFA;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(DetailsTopicPage), typeof(DetailsTopicPage));
    }
}
