using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CDH_GroundStation_Group6;

namespace CDH_GroundStation_Tests
{
    [TestClass]
    public class UserTests
    {
        // Constructor Tests
        [TestMethod]
        public void TestConstructor_AssignsValues()
        {
            // Arrange
            string username = "testUser";
            string password = "testPassword";

            // Act
            User user = new User(username, password);

            // Assert
            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void TestConstructor_AllowsEmptyUsername()
        {
            User user = new User("", "testPassword");
            Assert.AreEqual("", user.Username);
        }

        [TestMethod]
        public void TestConstructor_AllowsEmptyPassword()
        {
            User user = new User("testUser", "");
            Assert.AreEqual("", user.Password);
        }

        [TestMethod]
        public void TestConstructor_NullUsername()
        {
            User user = new User(null, "testPassword");
            Assert.IsNull(user.Username);
        }

        [TestMethod]
        public void TestConstructor_NullPassword()
        {
            User user = new User("testUser", null);
            Assert.IsNull(user.Password);
        }

        // Property Tests
        [TestMethod]
        public void TestUsernameProperty_SetGet()
        {
            User user = new User("testUser", "testPassword");
            user.Username = "newUser";
            Assert.AreEqual("newUser", user.Username);
        }

        [TestMethod]
        public void TestPasswordProperty_SetGet()
        {
            User user = new User("testUser", "testPassword");
            user.Password = "newPassword";
            Assert.AreEqual("newPassword", user.Password);
        }

        // Boundary Tests
        [TestMethod]
        public void TestUsername_MaxLength()
        {
            string longUsername = new string('a', 256); // Assuming max length 256
            User user = new User(longUsername, "testPassword");
            Assert.AreEqual(longUsername, user.Username);
        }

        [TestMethod]
        public void TestPassword_MaxLength()
        {
            string longPassword = new string('b', 256); // Assuming max length 256
            User user = new User("testUser", longPassword);
            Assert.AreEqual(longPassword, user.Password);
        }

        [TestMethod]
        public void TestUsername_SpecialCharacters()
        {
            string specialUsername = "user!@#";
            User user = new User(specialUsername, "testPassword");
            Assert.AreEqual(specialUsername, user.Username);
        }

        [TestMethod]
        public void TestPassword_SpecialCharacters()
        {
            string specialPassword = "pass!@#";
            User user = new User("testUser", specialPassword);
            Assert.AreEqual(specialPassword, user.Password);
        }

        // Equality Tests (if applicable)
        [TestMethod]
        public void TestUsers_WithIdenticalProperties_AreEqual()
        {
            User user1 = new User("sameUser", "samePass");
            User user2 = new User("sameUser", "samePass");
            Assert.IsTrue(user1.Equals(user2));
        }

        [TestMethod]
        public void TestUsers_WithDifferentProperties_AreNotEqual()
        {
            User user1 = new User("user1", "pass1");
            User user2 = new User("user2", "pass2");
            Assert.IsFalse(user1.Equals(user2));
        }
    }
}
