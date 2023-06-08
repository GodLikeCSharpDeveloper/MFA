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
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey)),
                SecurityAlgorithms.HmacSha256)
            )
            {
               
            };

            // Create payload
            var payload = new JwtPayload
            {
                { "aud", "application-0-tzqol" },
                { "sub", "1234567890" },
                { "name", "John Doe"},
                { "iat", "1516239022"}
                // Add additional claims as needed
            };

            // Create JWT
            var token = new JwtSecurityToken(header, payload);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            var encodedHeader = Base64UrlEncoder.Encode(header.ToString());
            var encodedPayload = Base64UrlEncoder.Encode(payload.ToString());
            var signatureInput = encodedHeader + "." + encodedPayload;
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(signingKey));
            var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(signatureInput));
            var signature = Base64UrlEncoder.Encode(signatureBytes);
            return $"{encodedHeader}.{encodedPayload}.{signature}"; ;
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
