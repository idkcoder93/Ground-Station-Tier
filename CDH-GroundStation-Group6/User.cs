using System;

namespace CDH_GroundStation_Group6
{
    internal class User: IUser
    {
        private string username;
        private string password;

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;

            // Assign the values to properties
            Username = username;
            Password = password;
        }

        // Getters and Setters
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
