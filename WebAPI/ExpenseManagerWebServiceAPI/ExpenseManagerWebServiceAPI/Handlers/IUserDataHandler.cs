using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface IUserDataHandler
    {
        bool createUser();
        bool updateUser();
        User getUser(int userId);
        bool checkUsername(string username);
        User findUser();
        User validateUser(string username);
    }
}
