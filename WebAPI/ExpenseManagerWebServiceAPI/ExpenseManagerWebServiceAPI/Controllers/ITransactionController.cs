using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface ITransactionController
    {
        JsonResult createTransaction(Transaction transaction);
        JsonResult updateTransaction(Transaction transaction);
        JsonResult deleteTransaction(int userId);
        JsonResult getAllActivity(int userId);
    }
}
