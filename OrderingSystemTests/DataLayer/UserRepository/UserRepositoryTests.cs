using DataLayer;
using Domain;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace OrderingSystemTests.DataLayer.UserRepository
{
    [TestFixture]
    public class UserRepositoryTests
    {
        private const string filePath = @"OrderingSystemTests\DataLayer\payloads\users.json";
        private PersistanceManager<User> userPersistanceManager = new PersistanceManager<User>(filePath);

        [SetUp]
        public void SetUp()
        {
            TestUtility.TestUtility.RemoveAllRecords(filePath);
        }

        [Test]
        public void AddEntity_WhenAddingOneEntity_Success()
        {
            // Arrange
            User user = this.GetValidUser();

            // Act
            this.userPersistanceManager.AddEntity(user);

            // Assert
            List<User> users = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(1, users.Count());
            this.AssertUser(user, users[0]);
        }

        [Test]
        public void AddEntity_WhenAddingMultipleEntities_Success()
        {
            // Arrange
            List<User> users = this.GetUserList();

            // Act
            foreach(User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            // Assert
            List<User> usersFromFile = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(3, users.Count());
            for(int i=0; i < usersFromFile.Count; i++)
            {
                this.AssertUser(users[i], usersFromFile[i]);
            }
        }

        [Test]
        public void DeleteEntity_Success()
        {
            // Arrange
            List<User> users = this.GetUserList();
            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            // Act
            this.userPersistanceManager.DeleteEntity(1);

            // Assert
            List<User> usersFromFile = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(2, usersFromFile.Count());
            for(int i=1; i< users.Count; i++)
            {
                this.AssertUser(users[i], usersFromFile[i-1]);
            }
        }

        [TestCase(1,4)]
        [TestCase(3,3)]
        public void AddEntity_SetAppropriateIdAfterDeleteItemFromList_Success(int userId, int expectedId)
        {
            List<User> users = this.GetUserList();
            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }
            this.userPersistanceManager.DeleteEntity(userId);

            // Act
            this.userPersistanceManager.AddEntity(new User()
            {
                IsAdmin = true,
                IsRegister = true,
                Name = "User4",
                UserName = "Username4",
                Password = "Password4",
                YearOfBorn = 1997
            });

            // Assert
            List<User> usersFromFile = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(3, usersFromFile.Count());
            Assert.AreEqual(expectedId, usersFromFile[2].Id);
        }

        [Test]
        public void GetEntityById_WhenUserWithIdExsist_ShouldReturnUser()
        {
            // Arrange
            List<User> users = this.GetUserList();

            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            // Act
            User result = this.userPersistanceManager.GetEntityById(1);

            // Assert
            this.AssertUser(users[0], result);
        }

        [Test]
        public void GetEntityById_WhenUserWithIdDoesNotExsist_ShouldReturnNull()
        {
            // Arrange
            List<User> users = this.GetUserList();

            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            // Act
            User result = this.userPersistanceManager.GetEntityById(15);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public void UpdateEntity_WhenUserExsist_ShouldUpdateUser()
        {
            // Arrange
            List<User> users = this.GetUserList();

            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            User userUpdate = new User()
            {
                Id = 1,
                IsAdmin = true,
                IsRegister = true,
                Name = "NewUser",
                UserName = "Username12",
                Password = "Password12",
                YearOfBorn = 1996
            };

            // Act
            this.userPersistanceManager.UpdateEntity(userUpdate);

            // Assert
            List<User> userFromFile = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(3, userFromFile.Count());
            this.AssertUser(userUpdate, userFromFile[0]);
            this.AssertUser(users[1], userFromFile[1]);
            this.AssertUser(users[2], userFromFile[2]);
        }

        [Test]
        public void UpdateEntity_WhenUserDoesNotExsist_ShouldReturnOriginalList()
        {
            // Arrange
            List<User> users = this.GetUserList();

            foreach (User user in users)
            {
                this.userPersistanceManager.AddEntity(user);
            }

            // Act
            this.userPersistanceManager.UpdateEntity(new User() { Id = 12 });

            // Assert
            List<User> userFromFile = this.userPersistanceManager.GetAllEntities().ToList();
            Assert.AreEqual(3, userFromFile.Count());
            this.AssertUser(users[0], userFromFile[0]);
            this.AssertUser(users[1], userFromFile[1]);
            this.AssertUser(users[2], userFromFile[2]);
        }
        #region
        private User GetValidUser()
        {
            return new User
            {
                IsAdmin = true,
                IsRegister = true,
                Name = "User",
                UserName = "Username",
                Password = "Password",
                YearOfBorn = 1997
            };
        }

        private List<User> GetUserList()
        {
            List<User> users = new List<User>()
            {
                this.GetValidUser(),
                new User(){IsAdmin=true, IsRegister = false, Name= "User1", UserName = "Username1", Password = "Password1", YearOfBorn = 2000},
                new User(){IsAdmin=true, IsRegister = false, Name= "User2", UserName = "Username2", Password = "Password2", YearOfBorn = 1955}
            };
            return users;
        }
        private void AssertUser(User actual, User expected)
        {
            Assert.AreEqual(actual.Id, expected.Id);
            Assert.AreEqual(actual.IsAdmin, expected.IsAdmin);
            Assert.AreEqual(actual.IsRegister, expected.IsRegister);
            Assert.AreEqual(actual.Name, expected.Name);
            Assert.AreEqual(actual.Password, expected.Password);
            Assert.AreEqual(actual.YearOfBorn, expected.YearOfBorn);
        }
        #endregion
    }
}
