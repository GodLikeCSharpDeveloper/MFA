using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFA.Services.UserService
{
    public interface IUserDbService
    {
        public async Task<User> AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser(User currentUser, User updatedUserInfo)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
