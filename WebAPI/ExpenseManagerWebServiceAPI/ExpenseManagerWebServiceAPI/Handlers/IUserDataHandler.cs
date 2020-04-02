using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface IUserDataHandler
    {
        int createUser();
        bool updateUser();
    }
}
