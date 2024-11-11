using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDH_GroundStation_Group6
{
    public interface IUser
    {
        string Username { get; }
        string Password { get; }
    }
    public interface IDatabase
    {
        Task<bool> SearchUserInDB(IUser user);
    }
}

