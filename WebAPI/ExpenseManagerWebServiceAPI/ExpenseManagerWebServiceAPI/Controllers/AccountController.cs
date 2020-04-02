using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Models;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class AccountController : Controller, IAccountController
    {
        private readonly IConfiguration config;
        public AccountController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        public JsonResult createAccount([FromBody] Account account)
        {
            AccountDataHandler accountDataHandler = new AccountDataHandler(config);
            accountDataHandler.userId = account.userId;
            accountDataHandler.firstName= account.firstName;
            accountDataHandler.lastName = account.lastName;
            accountDataHandler.email = account.email;
            accountDataHandler.phone = account.phone;
            bool result = accountDataHandler.createAccount();
            Response response = new Response();
            response.status = result;
            return Json(response);
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getAccount(int userId)
        {
            Response response = new Response();
            try
            {
                AccountDataHandler accountDataHandler = new AccountDataHandler(config);
                Account account = accountDataHandler.getAccount(userId);
                response.status = true;
                response.data = JsonConvert.SerializeObject(account);
            }
            catch (Exception ex) {
                response.message = ex.Message;
                response.status = false;
            }
            return Json(response);            
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateAccount(Account account)
        {
            throw new NotImplementedException();
        }
    }
}