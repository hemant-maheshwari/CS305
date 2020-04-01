using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface ITotalController
    {
        //function will create user for first time with total income and expense as 0.
        JsonResult createTotal(Total total);
        JsonResult updateTotal(Total total);
        JsonResult getTotal(int userId);
    }
}
