
namespace Domain
{
    public class LogInEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public LogInEntity()
        {

        }

        public LogInEntity(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
