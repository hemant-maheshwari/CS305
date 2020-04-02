using ExpenseManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface IAccountDataHandler
    {
        bool createAccount();
        bool updateAccount();
        Account getAccount(int userId);
    }
}
