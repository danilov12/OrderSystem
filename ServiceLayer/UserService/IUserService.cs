using Domain;
using System.Collections.Generic;

namespace ServiceLayer.UserService
{
    public interface IUserService
    {
        void AddUser(User user);
        void DeleteUser(int id);
        void UpdateUser(User user);
        List<User> GetAllUsers();
        User GetUserById(int id);
        void RegisterUser(User user);
        User GetUserByUserName(string userName);
    }
}
