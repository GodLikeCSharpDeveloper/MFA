﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MFA.Services.DBService;
using Realms.Sync;
using User = MFA.Models.User;

namespace MFA.Services.UserService
{
    public class UserDbService : IUserDbService
    {
        public async Task<User> AddUser(User user)
        {
            using var realm = RealmService.GetRealm();
            await realm.WriteAsync(() =>
            {
                realm.Add(user);
            });
            return user;
        }

        public List<User> GetAllUsersAsync()
        {
            using var realm = RealmService.GetRealm();
            return realm.All<User>().ToList();
        }

        public User GetUserByEmail(string email)
        {
            var realm = RealmService.GetRealm();
            var user = realm.All<User>().FirstOrDefault(x => x.Email == email);
            return user;
        }
        public async Task<User> UpdateUser(User currentUser, User updatedUserInfo)
        {
            using var realm = RealmService.GetRealm();
            var userForUpdate = GetUserByEmail(currentUser.Email);
            await realm.WriteAsync(() =>
            {
                userForUpdate.UsersImage = updatedUserInfo.UsersImage ?? userForUpdate.UsersImage;
                userForUpdate.Email = updatedUserInfo.Email ?? userForUpdate.Email;
                userForUpdate.Name = updatedUserInfo.Name ?? userForUpdate.Name;
                userForUpdate.Password = updatedUserInfo.Password ?? userForUpdate.Password;
                userForUpdate.Address = updatedUserInfo.Address ?? userForUpdate.Address;
            });
            if (realm.SyncSession.ConnectionState != ConnectionState.Disconnected)
            {
                await realm.Subscriptions.WaitForSynchronizationAsync();
            }
            return userForUpdate;
        }
    }
}
