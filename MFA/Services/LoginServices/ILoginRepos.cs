﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.LoginServices
{
    public interface ILoginRepos
    {
        public async Task<bool> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static async Task LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static async Task LogoutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
