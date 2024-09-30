using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDH_GroundStation_Group6
{
    internal class User
    {
        private string userID;
        private string password;
        private uint age;

        public string getUserID () { return userID; }
        public string getPassword () { return password; }
        public uint getAge () { return age; }
        public void setUserID(string userID) { userID = this.userID; }
        public void setPassword(string password) { this.password = password; }
        public void setAge(uint age) { this.age = age; }
    }
}
