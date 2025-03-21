using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle_Management_System.BusinessLogic
{
    public interface IUserAuthentication
    {
        bool Login(string userId, string password);
        void Logout();
    }
}
