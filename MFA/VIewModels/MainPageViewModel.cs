using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MFA.Models;
using MFA.Services;
using MFA.Views;


namespace MFA.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel

    {
    public ObservableCollection<Topic> Topics { get; }
    private TopicService service;
    UserRepository userRepository;

    public MainPageViewModel(TopicService service, UserRepository userRepository)
    {
        this.service = service;
        Topics = new(service.GenerateInfo(25));
        this.userRepository = userRepository;
            userRepository.AddUser();
        
    }

    [RelayCommand]
    public async Task GoToDetailsAsync(Topic topic)
    {
        await Shell.Current.GoToAsync(nameof(DetailsTopicPage), true, new Dictionary<string, object>
        {
            { "Topic", topic }
        });
    }
    }
}
