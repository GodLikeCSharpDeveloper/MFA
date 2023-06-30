using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MFA.Models;
using MFA.Services;
using MFA.Services.DBService;
using MFA.Services.NavigationService;
using MFA.Services.NotificationService;
using MFA.Views;


namespace MFA.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;

        public ObservableCollection<Topic> Topics =>
            new(topicDbService.GetAllTopics().Take(10));


        TopicService service;
        INavigationRepository navigationRepository;
        ITopicDBService topicDbService;
        INotificationService notificationService;
        public static User User { get; set; }

        public MainPageViewModel(TopicService service, INavigationRepository navigationRepository, ITopicDBService topicDbService, INotificationService notificationService)
        {
            this.service = service;
            this.navigationRepository = navigationRepository;
            this.topicDbService = topicDbService;
            this.notificationService = notificationService;
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
        [RelayCommand]
        public async Task Refresh()
        {
            try
            {
                IsRefreshing = true;
                _ = Topics;
            }
            catch (Exception ex)
            {
                await notificationService.Notify(ex.Message);
            }
            finally
            {
                IsRefreshing = false;
            }
        }

    }
}
