using Domain;

namespace ServiceLayer.LogInSystem
{
    public interface ILogInService
    {
        void LogInUser(LogInEntity user);
        void LogOutUser();
    }
}
