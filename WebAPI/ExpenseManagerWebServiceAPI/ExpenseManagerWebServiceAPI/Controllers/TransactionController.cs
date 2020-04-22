using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.Handlers;
using ExpenseManagerWebServiceAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Models;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class TransactionController : Controller, ITransactionController
    {
        private readonly IConfiguration config;

        public TransactionController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createTransaction([FromBody] Transaction transaction)
        {
            return saveTransaction(transaction, "create");
        }

        public JsonResult deleteTransaction(int userId)
        {
            throw new NotImplementedException();
        }

        [Route("getAllActivity/{userId}")]
        [HttpGet]
        public JsonResult getAllActivity(int userId)
        {
            Response response = new Response();
            try
            {
                TransactionDataHandler transactionDataHandler = new TransactionDataHandler(config);
                List<ActivityViewModel> activityViewModels = transactionDataHandler.getAllActivity(userId);
                response.status = true;
                response.data = JsonConvert.SerializeObject(activityViewModels);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        private JsonResult saveTransaction(Transaction transaction, string command)
        {
            bool result = false;
            TransactionDataHandler transactionDataHandler = new TransactionDataHandler(config);
            transactionDataHandler.transactionId = transaction.transactionId;
            transactionDataHandler.userId = transaction.userId;
            transactionDataHandler.title = transaction.title;
            transactionDataHandler.type = transaction.type;
            transactionDataHandler.amount = transaction.amount;
            transactionDataHandler.friendId = transaction.friendId;
            transactionDataHandler.transactionPicture = transaction.transactionPicture;
            transactionDataHandler.dateCreated = transaction.dateCreated;
            transactionDataHandler.dateUpdated = transaction.dateUpdated;
            if (command.Equals("create"))
            {
                result = transactionDataHandler.createTransaction();
            }            
            Response response = new Response();
            response.status = result;
            return Json(response);
        }
    }
}