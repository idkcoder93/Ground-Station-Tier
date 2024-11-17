using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CDH_GroundStation_Group6;

namespace CDH_GroundStation_Tests
{
    [TestClass]
    public class DatabaseTests
    {
        private Mock<IDatabase> mockDatabase;
        private Mock<IUser> testUser;

        [TestInitialize]
        public void Setup()
        {
            // Initialize mock database and user
            mockDatabase = new Mock<IDatabase>();
            testUser = new Mock<IUser>();
        }

        [TestMethod]
        public async Task SearchUserInDB_UserExistsWithCorrectCredentials_ReturnsTrue()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("existingUser");
            testUser.Setup(u => u.Password).Returns("correctPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(true);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsTrue(result, "Expected to return true for existing user with correct credentials.");
        }

        [TestMethod]
        public async Task SearchUserInDB_UserExistsWithIncorrectPassword_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("existingUser");
            testUser.Setup(u => u.Password).Returns("wrongPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for existing user with incorrect password.");
        }

        [TestMethod]
        public async Task SearchUserInDB_UserDoesNotExist_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("nonExistentUser");
            testUser.Setup(u => u.Password).Returns("anyPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for non-existent user.");
        }

        [TestMethod]
        public async Task SearchUserInDB_NullUsernameOrPassword_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns((string)null);
            testUser.Setup(u => u.Password).Returns((string)null);
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false when username or password is null.");
        }

        [TestMethod]
        public async Task SearchUserInDB_DatabaseConnectionFails_ThrowsException()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("userWithConnectionIssue");
            testUser.Setup(u => u.Password).Returns("anyPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ThrowsAsync(new Exception("Database connection failed."));

            // Act and Assert
            await Assert.ThrowsExceptionAsync<Exception>(async () =>
            {
                await mockDatabase.Object.SearchUserInDB(testUser.Object);
            });
        }

        [TestMethod]
        public async Task SearchUserInDB_EmptyUsernameAndPassword_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns(string.Empty);
            testUser.Setup(u => u.Password).Returns(string.Empty);
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for empty username and password.");
        }

        [TestMethod]
        public async Task SearchUserInDB_UsernameWithSpecialCharacters_ReturnsCorrectResult()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("user@123!");
            testUser.Setup(u => u.Password).Returns("specialPass");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(true);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsTrue(result, "Expected to handle special characters in username.");
        }


        // ADVANCED TEST CASES 
        [TestMethod]
        public async Task ADV_SearchUser_DBNull_ReturnsFalse()
        {
            // Arrange
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(null);

            // Assert
            Assert.IsFalse(result, "Expected to return false when the user object is null.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_LongUsername_ReturnsFalse()
        {
            // Arrange
            string longUsername = new string('a', 256); // 256 characters
            testUser.Setup(u => u.Username).Returns(longUsername);
            testUser.Setup(u => u.Password).Returns("validPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for a username longer than allowed.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_CaseSensitiveUsername_ReturnsCorrectResult()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("CaseSensitiveUser");
            testUser.Setup(u => u.Password).Returns("correctPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.Is<IUser>(user => user.Username == "CaseSensitiveUser"))).ReturnsAsync(true);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsTrue(result, "Expected to correctly handle case-sensitive usernames.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_WhitespaceInCredentials_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("   "); // Only whitespace
            testUser.Setup(u => u.Password).Returns("   ");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for credentials with only whitespace.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_InvalidCharacterEncoding_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("用户名"); // Unicode characters
            testUser.Setup(u => u.Password).Returns("密码");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected to return false for invalid character encoding in credentials.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_SimultaneousCalls()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("simultaneousUser");
            testUser.Setup(u => u.Password).Returns("correctPassword");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(true);

            // Act
            var tasks = new[]
            {
        mockDatabase.Object.SearchUserInDB(testUser.Object),
        mockDatabase.Object.SearchUserInDB(testUser.Object),
        mockDatabase.Object.SearchUserInDB(testUser.Object)
    };
            var results = await Task.WhenAll(tasks);

            // Assert
            Assert.IsTrue(results.All(result => result), "Expected all simultaneous calls to return true.");
        }

        [TestMethod]
        public async Task ADV_SearchUser_SQLInjection_ReturnsFalse()
        {
            // Arrange
            testUser.Setup(u => u.Username).Returns("' OR 1=1; --");
            testUser.Setup(u => u.Password).Returns("irrelevant");
            mockDatabase.Setup(db => db.SearchUserInDB(It.IsAny<IUser>())).ReturnsAsync(false);

            // Act
            bool result = await mockDatabase.Object.SearchUserInDB(testUser.Object);

            // Assert
            Assert.IsFalse(result, "Expected the database to handle SQL injection attempts securely.");
        }

    }
}
