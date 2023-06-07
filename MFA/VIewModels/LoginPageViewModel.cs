using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bogus.DataSets;
using MFA.Services.LoginServices;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        RealmLoginRepository repository;
        public LoginPageViewModel(RealmLoginRepository repository)
        {
            this.repository = repository;
        }
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;


        [RelayCommand]
        public async Task RegisterHandlerHandler()
        {
            if (!repository.RegisterAsync(Email, Password).Result)
                return;
            if (Email == "Admin@gmail.com" && Password == "Admin")
                await Shell.Current.GoToAsync("MainPage", true);
        }
    }
}//TODO ADD LOGIN ONLY FOR ONLINE DB AND SOME KIND OF TIMER TO LOGOUT MB, THEN WORKING WITH ADDING NEW USERS
