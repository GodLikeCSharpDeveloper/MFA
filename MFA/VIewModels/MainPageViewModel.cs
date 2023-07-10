using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MFA.Models;
using MFA.Services;
using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.NavigationService;
using MFA.Services.NotificationService;
using MFA.Services.UserService;
using MFA.Utility.UiHelper.CollectionUiLogic;
using MFA.Views;
using Realms;
using Realms.Sync;
using User = MFA.Models.User;


namespace MFA.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        bool isRefreshing;


        TopicService service;
        INavigationRepository navigationRepository;
        ITopicDBService topicDbService;
        INotificationService notificationService;
        IUserLogOut userLogOut;
        ICollectionUiLogic<Topic> collectionUiLogic;
        public static User User { get; set; }
        private List<Topic> topicList = new();
        public MainPageViewModel(TopicService service,
            INavigationRepository navigationRepository,
            ITopicDBService topicDbService,
            INotificationService notificationService,
            IUserLogOut userLogOut,
            ICollectionUiLogic<Topic> collectionUiLogic)
        {
            this.service = service;
            this.userLogOut = userLogOut;
            this.navigationRepository = navigationRepository;
            this.topicDbService = topicDbService;
            this.notificationService = notificationService;
            topicList = topicDbService.GetAllTopics();
            this.collectionUiLogic = collectionUiLogic;
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
                Topics = new(topicDbService.GetAllTopics().Take(30));
                topicCount = 30;
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

        int topicCount = 30;
        [ObservableProperty]
        public ObservableCollection<Topic> topics;
        public ICommand OnCollectionEndReachedCommand => new Command(OnCollectionEndReached);
        void OnCollectionEndReached()
        {
            collectionUiLogic.OnCollectionEndReached(topicList, Topics, ref topicCount);
        }
        [RelayCommand]
        private async Task LogOut()
        {
            await userLogOut.LogoutAsync();
            await navigationRepository.NavigateTo(nameof(LoginPage));
        }
        [RelayCommand]
        private async Task GoToTopicAdd()
        {
            await navigationRepository.NavigateTo(nameof(TopicAddOrRemove));
        }
        [RelayCommand]
        private async Task GoToUserInfo()
        {
            await navigationRepository.NavigateTo(nameof(UserInfoPage));
        }
    }
}
