using DataLayer;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServiceLayer.UserService
{
    public class UserService : IUserService
    {
        private IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this._userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new NullReferenceException("Greska prilikom regitracije administratora.");
            if (user.IsAdmin && !user.IsRegister)
                throw new ArgumentException("Administrator mora biti registrovan.");
            if (!user.IsAdmin && user.IsRegister)
                throw new ArgumentException("Kupca mora registrovati administrator.");
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Kupac mora imati username i password.");
            if (!this.IsUsernameUnique(user.UserName))
                throw new ArgumentException("Korisnik mora imati unikatno korisnicko ime.");

            this._userRepository.AddEntity(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
                throw new NullReferenceException("Greska prilikom izmene korisnika.");
            if (user.Id <= 0)
                throw new ArgumentException("Korisnik mora imati validan id.");
            if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentException("Kupac mora imati username i password.");
            if (!this.IsUsernameUnique(user.UserName))
                throw new ArgumentException("Korisnik mora imati unikatno korisnicko ime.");

            this._userRepository.UpdateEntity(user);
        }

        public void DeleteUser(int id)
        {
            this._userRepository.DeleteEntity(id);
        }

        public List<User> GetAllUsers()
        {
            return this._userRepository.GetAllEntities().ToList();
        }

        public User GetUserById(int id)
        {
            return this._userRepository.GetEntityById(id);
        }

        public void RegisterUser(User user)
        {
            if (!this.DoesUserExsist(user.UserName))
                throw new ArgumentException("Korisnik ne postoji u sistemu.");

            user.IsRegister = true;
            this._userRepository.UpdateEntity(user);
        }

        public User GetUserByUserName(string userName)
        {
            return this._userRepository
                .GetAllEntities()
                .Where(x => x.UserName == userName)
                .FirstOrDefault();
        }

        #region HelperMethods
        private bool IsUsernameUnique(string username)
        {
            return this._userRepository.GetAllEntities()
                .Where(x => x.UserName.Equals(username))
                .Count() == 0;
        }

        private bool DoesUserExsist(string userName)
        {
            return this.GetUserByUserName(userName) != null;
        }
        #endregion
    }
}
