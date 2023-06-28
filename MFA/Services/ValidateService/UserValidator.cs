using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace MFA.Services.ValidateService
{
    public class UserValidator : IUserValidator
    {
        public string Error { get; set; }
        string nameRules = @"[a-zA-Z0-9_.+-]{3,}";
        string emailRules = @"[a-zA-Z0-9_.+-]+@([a-zA-Z0-9-])?(mail)\.([a-z]){2,3}$";
        public bool Validator(User user)
        {
            var sb = new StringBuilder();
            var emailBool = RegexValidation(user.Email, emailRules);
            var nameBool = RegexValidation(user.Name, nameRules);
            var passwordBool = !string.IsNullOrEmpty(user.Password);
            var addressBool = !string.IsNullOrEmpty(user.Address);
            var testbool = true;
            var properties = typeof(User)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(ø => ø.PropertyType == typeof(string)).ToArray();
            List<bool> bools = new List<bool>()
            {
                testbool,nameBool,emailBool,passwordBool,addressBool
            };
            for (int i = 0; i < properties.Count(); i++)
            {
                if (!bools[i])
                {
                    if (i == properties.Count() - 1)
                        sb.Append($"{properties[i].Name.ToLower()}");
                    else sb.Append($"{properties[i].Name.ToLower()}, ");

                }
            }
            Error = sb.ToString();
            if (!string.IsNullOrEmpty(Error))
            {
                Shell.Current.DisplayAlert("Error!", $"Following fields are entered incorrect: {Error}", "Ok");
                return false;
            }
            return true;
        }

        public bool Validator(string email, string password)
        {
            var IsValid = RegexValidation(email, emailRules);
            if (string.IsNullOrEmpty(password)&&!IsValid)
            {
                Shell.Current.DisplayAlert("Error!", $"Enter a valid email or password", "Ok");
                return false;
            }
            return true;
        }

        public bool RegexValidation(string text, string pattern)
        {
            bool regexEmailBool = false;
            var isNullBool = !string.IsNullOrEmpty(text);
            if (isNullBool)
                regexEmailBool = Regex.IsMatch(text, pattern);
            return regexEmailBool;
        }
    }
}
