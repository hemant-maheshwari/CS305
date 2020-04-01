using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class TransactionController : Controller, ITransactionController
    {
        private readonly IConfiguration config;

        public TransactionController(IConfiguration config) {
            this.config = config;
        }
        public JsonResult createTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public JsonResult deleteTransaction(int userId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
    }
}