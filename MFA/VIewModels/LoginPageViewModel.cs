using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bogus.DataSets;
using MFA.Services.DBService;
using MFA.Services.LoginServices;
using MFA.Services.LoginServices.RegisterRepos;
using MFA.Views;


namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        ILoginValidator validator;

        public LoginPageViewModel(ILoginValidator validator)
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
            if (!validator.Validator(email, password)) return;
            RealmLoginRepository.LoginAsync(Email, Password);
            {
                await Shell.Current.GoToAsync("MainPage", true);
            }
        }
        [RelayCommand]
        public async Task RegisterHandler()
        {
            var user = new User()
            {
                Name = "tomas",
                Email = Email,
                Password = Password,
                Address = "london"
            };
            await RealmRegisterRepos.RegisterUser(user);
            if (!validator.Validator(email, password)) return;
            await Shell.Current.GoToAsync("MainPage", true);
        }
    }
}
