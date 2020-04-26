using System;
using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Models;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class TotalController : Controller, ITotalController
    {
        private readonly IConfiguration config;
        public TotalController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createTotal([FromBody] Total total)
        {
            return saveTotal(total, "create_total");
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getTotal(int userId)
        {
            Response response = new Response();
            try {
                TotalDataHandler totalDataHandler = new TotalDataHandler(config);
                Total total = totalDataHandler.getTotal(userId);
                response.status = true;
                response.data = JsonConvert.SerializeObject(total);
            } catch (Exception ex) {
                response.status = false;
                response.message = ex.Message;
            }
            return Json(response);            
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateTotal([FromBody] Total total)
        {
            return saveTotal(total, "update_total");
        }

        private JsonResult saveTotal(Total total, string command) {
            bool result = false;
            Response response = new Response();
            TotalDataHandler totalDataHandler = new TotalDataHandler(config);
            totalDataHandler.userId = total.userId;
            totalDataHandler.expenseAmount = total.expenseAmount;
            totalDataHandler.incomeAmount = total.incomeAmount;
            if (command.Equals("create_total")) {
                result = totalDataHandler.createTotal();
            }
            if (command.Equals("update_total")) {
                result = totalDataHandler.updateTotal();
            }
            response.status = result;
            return Json(response);
        }
    }
}