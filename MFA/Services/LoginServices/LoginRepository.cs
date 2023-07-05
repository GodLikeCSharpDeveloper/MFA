using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MFA.Services.DBService;
using MFA.Services.ValidateService;
using Microsoft.IdentityModel.Tokens;
using Realms;
using Realms.Sync;
using static System.Net.Mime.MediaTypeNames;

namespace MFA.Services.LoginServices
{
    public class LoginRepository : ILoginRepos
    {
        public async Task LoginAsync(Models.User user)
        {
            //This will populate the initial set of subscriptions the first time the realm is opened
            await RealmService.app.LogInAsync(Credentials.EmailPassword(user.Email, user.Password));
            
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

    }
}
