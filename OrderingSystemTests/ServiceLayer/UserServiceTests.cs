using DataLayer;
using Domain;
using NSubstitute;
using NUnit.Framework;
using ServiceLayer.UserService;
using System;

namespace OrderingSystemTests.ServiceLayer
{
    [TestFixture]
    public class UserServiceTests
    {
        private IRepository<User> _userRepository;
        private UserService userService;

        [SetUp]
        public void SetUp()
        {
            this._userRepository = Substitute.For<IRepository<User>>();
            this.userService = new UserService(this._userRepository);
        }

        #region AddUserTests
        [Test]
        public void AddUser_WhenUserIsNull_ShouldThrowNullReferenceException()
        {
            // Arrange
            User user = null;

            // Act + Assert
            Assert.Throws<NullReferenceException>(() => this.userService.AddUser(user), "Greska prilikom regitracije administratora.");
        }

        [Test]
        public void AddUser_WhenAdminIsNotRegister_ShouldThrowArgumentException()
        {
            // Arrange
            User user = new User
            {
                IsAdmin = true,
                IsRegister = false
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this.userService.AddUser(user), "Administrator mora biti registrovan.");
        }

        [Test]
        public void AddUser_WhenCustomerIsRegistered_ShouldThrowArgumentException()
        {
            // Arrange
            User user = new User
            {
                IsAdmin = false,
                IsRegister = true
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this.userService.AddUser(user), "Kupca mora registrovati administrator.");
        }

        [TestCase("", "password")]
        [TestCase("username", "")]
        public void AddUser_WhenUserNameOrPasswordAreEmpty_ShouldThrowException(string username, string password)
        {
            // Arrange
            User user = new User
            {
                IsAdmin = false,
                IsRegister = false,
                UserName = username,
                Password = password
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this.userService.AddUser(user), "Kupac mora imati username i password.");
        }

        [Test]
        public void AddUser_Success()
        {
            // Arrange
            User user = this.GetValidUser();

            // Act
            this.userService.AddUser(user);

            // Assert
            this._userRepository.Received(1).AddEntity(Arg.Is(user));
        }
        #endregion

        #region UpdateUserTests
        [Test]
        public void UpdateUser_WhenUserIsNull_ShouldThrowException()
        {
            // Arrange
            User user = null;

            // Act + Assert
            Assert.Throws<NullReferenceException>(() => this.userService.UpdateUser(user), "Greska prilikom izmene korisnika.");
        }

        [Test]
        public void UpdateUser_WhenIdIsNotValid_ShouldThrowException()
        {
            // Arrange
            User user = new User();

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this.userService.UpdateUser(user), "Korisnik mora imati validan id.");
        }

        [TestCase("", "password")]
        [TestCase("username", "")]
        public void UpdateUser_WhenUserNameOrPasswordAreEmpty_ShouldThrowException(string username, string password)
        {
            // Arrange
            User user = new User
            {
                IsAdmin = false,
                IsRegister = false,
                UserName = username,
                Password = password
            };

            // Act + Assert
            Assert.Throws<ArgumentException>(() => this.userService.UpdateUser(user), "Kupac mora imati username i password.");
        }

        [Test]
        public void UpdateUser_Success()
        {
            // Arrange
            User user = this.GetValidUser();

            // Act 
            this.userService.UpdateUser(user);

            // Assert
            this._userRepository.Received(1).UpdateEntity(Arg.Is(user));
        }
        #endregion

        #region Helper
        private User GetValidUser()
        {
            return new User
            {
                Id = 1,
                IsAdmin = true,
                IsRegister = true,
                Name = "User",
                UserName = "Username",
                Password = "Password",
                YearOfBorn = 1997
            };
        }
        #endregion

    }
}
