using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.NavigationService;
using MFA.Services.ValidateService;

namespace MFA.Services.RegisterServices
{
    public class RegisterManager : IRegisterManager
    {
        INavigationRepository navigationRepository;
        IUserValidator userValidator;
        IRegisterRepository registerRepository;
        public RegisterManager(INavigationRepository navigationRepository, IUserValidator userValidator, IRegisterRepository registerRepository)
        {
            this.navigationRepository = navigationRepository;
            this.userValidator = userValidator;
            this.registerRepository = registerRepository;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            if (!userValidator.Validator(user)) return null;
            await registerRepository.RegisterUser(user);
            await navigationRepository.NavigateTo("..");
            await navigationRepository.NavigateTo("..");
            return user;
        }
    }
}
