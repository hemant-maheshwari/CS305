using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface IUserController
    {
        JsonResult createUser(User user);
        JsonResult updateUser(User user);
        JsonResult getUser(int userId);
        JsonResult checkUsername(string username);
        JsonResult login(User user);
        JsonResult validateUsername(string username);

    }
}
