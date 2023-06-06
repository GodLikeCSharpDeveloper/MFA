using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Bogus.DataSets;
using MFA.Utility;
using MFA.Views;

namespace MFA.ViewModels
{
    public partial class LoginPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;

        public LoginPageViewModel()
        {
            
            
        }

        private string Error;
        public bool LoginValidator(string login, string password)
        {
            var sb = new StringBuilder("Enter ");
            string emailRules = @"[a-zA-Z0-9_.+-]+@([a-zA-Z0-9-])?(mail)\.([a-z]){2,3}$";
            var loginBool = string.IsNullOrEmpty(login);
            var passwordBool = string.IsNullOrEmpty(password);
            var regexBool = false;
            if (!loginBool)
            {
                regexBool = Regex.IsMatch(login, emailRules);
            }

            if (loginBool)
            {
                sb.Append("an email");
                if (passwordBool)
                    sb.Append(" and a password");
            }
            if (!regexBool&&!loginBool)
            {
                sb.Append("a valid email");
                if (passwordBool)
                    sb.Append(" and a password");
            }
            if (!loginBool && regexBool && passwordBool)
                sb.Append("a password");
            Error = sb.ToString();
            if (loginBool || passwordBool || !regexBool)
            {
                Shell.Current.DisplayAlert("Error!", Error, "Ok");
                return false;
            }
            return true;
        }

        [RelayCommand]
        public async Task LoginHandler()
        {
            if (!LoginValidator(Email, Password))
                return;
            if (Email == "Admin@gmail.com" && Password == "Admin")
                await Shell.Current.GoToAsync("..", true);

        }
    }
}
