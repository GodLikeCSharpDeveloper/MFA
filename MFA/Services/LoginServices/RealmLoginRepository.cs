using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Amazon.Runtime.Internal.Transform;
using MFA.Services.DBService;
using Microsoft.IdentityModel.Tokens;
using Realms;
using Realms.Sync;
using static System.Net.Mime.MediaTypeNames;

namespace MFA.Services.LoginServices
{
    public class RealmLoginRepository : ILoginRepos
    {
        ILoginValidator validator;

        public RealmLoginRepository(ILoginValidator loginValidator)
        {
            validator = loginValidator;


        }
        public static async Task RegisterAsync(string email, string password)
        {
            //if (!validator.Validator(email, password))
            //    return;
            if (email == "Admin@gmail.com" && password == "Admin")
            {
                await Shell.Current.GoToAsync("MainPage", true);
                await RealmService.app.EmailPasswordAuth.RegisterUserAsync(email, password);
            }
            
        }

        //public string GenerateToken(Models.User user)
        //{
        //    // Create header
        //    var header = new JwtHeader(new SigningCredentials(
        //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("tdJkuTY57hg2LbPewfewefewrgh543534g345yhgr6j56h5")),
        //        SecurityAlgorithms.HmacSha256));
        //    // Create payload
        //    var payload = new JwtPayload
        //    {
        //        { "aud", "application-0-khskk" },
        //        { "sub", "64848a81db4a00a9f34a4ea5" },
        //        { "name", "qweqwe" },
        //        // Add additional claims as needed
        //    };
        //    // Create JWT
        //    var token = new JwtSecurityToken(header, payload);
        //    var asda = new HMACSHA256();
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenString = tokenHandler.WriteToken(token);
        //    return tokenString;
        //}
        public static async Task LoginAsync(string email, string password)
        {
            //This will populate the initial set of subscriptions the first time the realm is opened

            await RealmService.app.LogInAsync(Credentials.EmailPassword(email, password));
            using var realm = RealmService.GetRealm();
            await realm.Subscriptions.WaitForSynchronizationAsync();
        }

        public static async Task LogoutAsync()
        {
            await RealmService.app.CurrentUser.LogOutAsync();
            RealmService.MainThreadRealm?.Dispose();
            RealmService.MainThreadRealm = null;
        }
    }
}
