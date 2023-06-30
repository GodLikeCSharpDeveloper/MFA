using MFA.Services.DBService;

namespace MFA;

public partial class App : Application
{
	public App()
	{
        ManualResetEvent m = new ManualResetEvent(false);
        Task.Factory.StartNew(async () =>
        {
            await RealmService.Init();
            m.Set();
        });

        m.WaitOne();
        InitializeComponent();

		MainPage = new AppShell();
    }
}
