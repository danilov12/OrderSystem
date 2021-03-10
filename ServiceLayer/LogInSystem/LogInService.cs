using Domain;
using ServiceLayer.UserService;
using System;
using Utility.Cache;

namespace ServiceLayer.LogInSystem
{
    public class LogInService : ILogInService
    {
        private IUserService _userService;
        private ICache _cache;

        public LogInService(IUserService userService, ICache cache)
        {
            this._userService = userService;
            this._cache = cache;
        }
        public void LogInUser(LogInEntity userLogInData)
        {
            if (string.IsNullOrEmpty(userLogInData.UserName) || string.IsNullOrEmpty(userLogInData.Password))
                throw new ArgumentException("Korisnicko ime i password su neophodni da bi se ulogovali.");
            if (!this.DoesUserExist(userLogInData))
                throw new ArgumentException("Korisnik ne postoji u bazi.");
            if (!this.IsPasswordCorrect(userLogInData))
                throw new ArgumentException("Pogresno korisnicko ime.");

            User user = this._userService.GetUserByUserName(userLogInData.UserName);
            this.AddUserToCache(user);
        }

        public void LogOutUser()
        {
            this._cache.EmptyCache();
        }

        #region HelperMethods
        private bool DoesUserExist(LogInEntity userLogInData)
        {
            return this._userService.GetUserByUserName(userLogInData.UserName) != null;
        }

        private bool IsPasswordCorrect(LogInEntity userLogInData)
        {
            return this._userService.GetUserByUserName(userLogInData.UserName).Password == userLogInData.Password;
        }

        private void AddUserToCache(User user)
        {
            if (user.IsAdmin)
            {
                this._cache.AddUpdateCache("Admin", user);
            }
            else
            {
                this._cache.AddUpdateCache("Customer", user);
            }
        }
        #endregion
    }
}
