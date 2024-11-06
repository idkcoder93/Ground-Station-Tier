using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    class Status
    {
        private string statusState = "OFFLINE"; // status indicator notifying if we're online by default offline

        public string StatusState
        {
            get { return statusState; }
            set { statusState = value; }
        }
    }
}
