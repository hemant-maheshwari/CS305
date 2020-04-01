using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface IAccountController
    {
        JsonResult createAccount(Account account);
        JsonResult updateAccount(Account account);
        JsonResult getAccount(int userId);
    }
}
