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

    private void Button_Clicked(object sender, EventArgs e)
    {
        TopicDbService serv = new TopicDbService();
        serv.AddNewTopic(new Topic
        {
            TopicContent = "aslkdjkladjlaksd",
            TopicTitle = "Title",
            TopicReleaseDate = "al;skjdlkasd",
            TopicUpdateDate = "adslkjdsakl",
            OwnerId = "648d6fa8428ec157a4b5586d"
        });
    }
}