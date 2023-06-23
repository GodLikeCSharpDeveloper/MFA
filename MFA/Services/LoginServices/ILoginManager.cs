using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LoginServices
{
    public interface ILoginManager
    {
        public async Task<User> LoggingUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
