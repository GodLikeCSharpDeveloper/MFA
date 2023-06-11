using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LoginServices
{
    public interface ILoginValidator
    {
        public string Error{ get; set; }
        public bool Validator(string login, string password);
    }
}
