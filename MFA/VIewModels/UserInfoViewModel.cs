using MFA.Services.NavigationService;
using MFA.Services.UserService;
using MFA.Utility.ImageManager;

namespace MFA.ViewModels
{

    public partial class UserInfoViewModel : BaseViewModel
    {
        IImageManager imageManager;
        IUserDbService userDbService;
        INavigationRepository navigationRepository;
        public UserInfoViewModel(IImageManager imageManager, IUserDbService userDbService, INavigationRepository navigationRepository)
        {
            this.imageManager = imageManager;
            this.userDbService = userDbService;
            this.navigationRepository = navigationRepository;
        }
        [ObservableProperty]
        public byte[] avatarData;
        [ObservableProperty]
        public User currentUser;

        [RelayCommand]
        public async Task ChangeAvatar()
        {
            var image = await imageManager.ChangeAvatar();
            await userDbService.UpdateUser(MainPageViewModel.User, new User
            {
                UsersImage = image
            });
            AvatarData = image.Data;
        }
        [RelayCommand]
        public async Task GoToUserEdit()
        {
            await navigationRepository.WaitingNavigateTo("UserEditPage", false);
        }
        [RelayCommand]
        public async void GoToTest()
        {
            await navigationRepository.NavigateTo("TopicAddOrRemove");
        }
    }
}

