using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;

namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        IUserValidator validator;

        public LoginPageViewModel(IUserValidator validator)
        {
            this.validator = validator;
            RealmService.Init();
        }
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string address;

        [RelayCommand]
        public async Task LoginHandler()
        {
            var user = new User()
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Address = Address
            };
            if (!validator.Validator(user.Email, user.Password)) return;
            try
            {
                await RealmLoginRepository.LoginAsync(user.Email, user.Password);
                await Shell.Current.GoToAsync("MainPage", true);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "OK");
                await Shell.Current.GoToAsync("..", true);
            }
        }
        [RelayCommand]
        public async Task MoveToRegister()
        {
            await Shell.Current.GoToAsync("RegisterPage", true);
        }
    }
}
