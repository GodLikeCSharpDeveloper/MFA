using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime.Internal.Transform;
using MFA.Services.DBService;
using Microsoft.IdentityModel.Tokens;
using Realms.Sync;

namespace MFA.Services.LoginServices
{
    public class RealmLoginRepository
    {
        private LoginValidator validator;
        public RealmLoginRepository(LoginValidator loginValidator)
        {
            validator = loginValidator;
        }
        public async Task<bool> RegisterAsync(string email, string password)
        {
            if (!validator.Validator(email, password))
                return false;
            if (email == "Admin@gmail.com" && password == "Admin")
            {
                await Shell.Current.GoToAsync("MainPage", true);
                await RealmService.App.EmailPasswordAuth.RegisterUserAsync(email, password);
                return true;
            }
            return false;
        }

        public string GenerateToken(string uniqueUserId, string signingKey)
        {
            // Create header
            var header = new JwtHeader(new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tdJkuTY57hg2LbPewfewefewrgh543534g345yhgr6j56h5")),
                SecurityAlgorithms.HmacSha256));
            // Create payload
            var payload = new JwtPayload
            {
                { "aud", "application-0-tzqol" },
                { "sub", "1234567890" },
                { "name", "John Doe" },
                // Add additional claims as needed
            };
            // Create JWT
            var token = new JwtSecurityToken(header, payload);
            var asda = new HMACSHA256();
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        public async Task<bool> LoginAsync(string email, string password)
        {
            await RealmService.App.LogInAsync(Credentials.JWT(""));
            
            //This will populate the initial set of subscriptions the first time the realm is opened
            using var realm = RealmService.GetRealm();
            await realm.Subscriptions.WaitForSynchronizationAsync();
            return true;
        }

        public static async Task LogoutAsync()
        {
            await RealmService.App.CurrentUser.LogOutAsync();
            RealmService.MainThreadRealm?.Dispose();
            RealmService.MainThreadRealm = null;
        }
    }
}
