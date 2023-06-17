using MFA.Services.RegisterServices;
using MFA.Services.ValidateService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.ViewModels
{
    public partial class RegisterPageViewModel : BaseViewModel
    {
        IRegister repository;
        IUserValidator validator;
        public RegisterPageViewModel(IRegister repository, IUserValidator validator)
        {
            this.repository = repository;
            this.validator = validator;
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
        public async Task RegisterHandler()
        {
            var user = new User()
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Address = Address,
            };
            try
            {
                if (!validator.Validator(user)) return;
                await repository.RegisterUser(user);
                await Shell.Current.GoToAsync("..", true);
                await Shell.Current.GoToAsync("..", true);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.ToString(), "OK");
                await Shell.Current.GoToAsync("RegisterPage", true);
            }
        }

    }
}
