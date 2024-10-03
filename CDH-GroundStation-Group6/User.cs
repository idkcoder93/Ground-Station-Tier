using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDH_GroundStation_Group6
{
    internal class User(string username, string password)
    {
        private string username = username, password = password;

        // Getters and Setters
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
