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
    public class TotalController : Controller, ITotalController
    {
        private readonly IConfiguration config;
        public TotalController(IConfiguration config) {
            this.config = config;
        }
        public JsonResult createTotal(Total total)
        {
            throw new NotImplementedException();
        }

        public JsonResult getTotal(int userId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateTotal(Total total)
        {
            throw new NotImplementedException();
        }
    }
}