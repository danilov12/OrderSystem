
namespace Domain
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public short YearOfBorn { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsRegister { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(int id, string name, short yearOfBorn, bool isAdmin, bool isRegister, string userName, string password)
        {
            this.Id = id;
            this.Name = name;
            this.YearOfBorn = yearOfBorn;
            this.IsAdmin = isAdmin;
            this.IsRegister = isRegister;
            this.UserName = userName;
            this.Password = password;
        }
    }
}
