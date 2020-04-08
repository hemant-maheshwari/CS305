using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface IUserDataHandler
    {
        bool createUser();
        bool updateUser();
        User getUser(int userId);
    }
}
