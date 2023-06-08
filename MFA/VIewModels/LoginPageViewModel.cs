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
        public async Task RegisterHandler()
        {
            repository.GenerateToken("1234567890", "tdJkuTY57hg2LbPewfewefewrgh543534g345yhgr6j56h5");
            if (!repository.LoginAsync(Email, Password).Result)
                return;
            if (Email == "Admin@gmail.com" && Password == "Admin")
                await Shell.Current.GoToAsync("MainPage", true);
        }
    }
}
