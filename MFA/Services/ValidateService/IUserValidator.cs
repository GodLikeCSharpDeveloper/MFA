using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.ValidateService
{
    public interface IUserValidator
    {
        public string Error { get; set; }
        public bool Validator(User user);
        public bool Validator(string email, string password);
    }
}
