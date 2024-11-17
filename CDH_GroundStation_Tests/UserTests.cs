using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CDH_GroundStation_Tests
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public override bool Equals(object obj)
        {
            if (obj is User otherUser)
            {
                return Username == otherUser.Username && Password == otherUser.Password;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Username, Password);
        }
    }

    [TestClass]
    public class UserTests
    {
        // Constructor Tests
        [TestMethod]
        public void TestConstructor_AssignsValues()
        {
            string username = "testUser";
            string password = "testPassword";
            User user = new User(username, password);

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
            string longUsername = new string('a', 256);
            User user = new User(longUsername, "testPassword");
            Assert.AreEqual(longUsername, user.Username);
        }

        [TestMethod]
        public void TestPassword_MaxLength()
        {
            string longPassword = new string('b', 256);
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

        // Equality Tests
        [TestMethod]
        public void TestUsers_WithDifferentProperties_AreNotEqual()
        {
            User user1 = new User("user1", "pass1");
            User user2 = new User("user2", "pass2");
            Assert.IsFalse(user1.Equals(user2));
        }

        // Advanced Constructor Tests
        [TestMethod]
        public void ADV_Constructor_HandlesWhitespace()
        {
            string username = "   ";
            string password = "   ";
            User user = new User(username, password);

            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(password, user.Password);
        }

        [TestMethod]
        public void ADV_Constructor_HandlesUnicode()
        {
            string username = "用户";
            string password = "密码";
            User user = new User(username, password);

            Assert.AreEqual(username, user.Username);
            Assert.AreEqual(password, user.Password);
        }

        // Advanced Property Tests
        [TestMethod]
        public void ADV_Username_AllowsNull()
        {
            User user = new User("testUser", "testPassword");
            user.Username = null;

            Assert.IsNull(user.Username);
        }

        [TestMethod]
        public void ADV_Password_AllowsNull()
        {
            User user = new User("testUser", "testPassword");
            user.Password = null;

            Assert.IsNull(user.Password);
        }

        [TestMethod]
        public void ADV_Equality_WithNullUser()
        {
            User user = new User("testUser", "testPassword");
            Assert.IsFalse(user.Equals(null));
        }

        [TestMethod]
        public void ADV_Equality_SameReference()
        {
            User user = new User("testUser", "testPassword");
            Assert.IsTrue(user.Equals(user));
        }

        [TestMethod]
        public void ADV_Equality_HandlesCaseSensitivity()
        {
            User user1 = new User("testUser", "testPassword");
            User user2 = new User("TESTUSER", "testPassword");
            Assert.IsFalse(user1.Equals(user2));
        }

        [TestMethod]
        public void ADV_Equality_DifferentWhitespace()
        {
            User user1 = new User("testUser", "testPassword");
            User user2 = new User(" testUser ", "testPassword");
            Assert.IsFalse(user1.Equals(user2));
        }

        [TestMethod]
        public void ADV_Equality_SpecialCharacters()
        {
            User user1 = new User("user@123!", "pass#456!");
            User user2 = new User("user@123!", "pass#456!");
            Assert.IsTrue(user1.Equals(user2));
        }
    }
}
