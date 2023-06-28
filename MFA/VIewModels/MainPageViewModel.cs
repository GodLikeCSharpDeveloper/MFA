using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MFA.Models;
using MFA.Services;
using MFA.Services.NavigationService;
using MFA.Views;


namespace MFA.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        public ObservableCollection<Topic> Topics { get; }
        TopicService service;
        INavigationRepository navigationRepository;
        public static User User { get; set; }

        public MainPageViewModel(TopicService service, INavigationRepository navigationRepository)
        {
            this.service = service;
            Topics = new(service.GenerateInfo(25));
            this.navigationRepository = navigationRepository;
        }

        [RelayCommand]
        public async Task GoToDetailsAsync(Topic topic)
        {
            await navigationRepository.NavigateTo(nameof(DetailsTopicPage), new Dictionary<string, object>
            {
                { "Topic", topic }
            });
        }
        [RelayCommand]
        public async Task TestBusy()
        {
            IsBusy = !IsBusy;
        }
    }
}
