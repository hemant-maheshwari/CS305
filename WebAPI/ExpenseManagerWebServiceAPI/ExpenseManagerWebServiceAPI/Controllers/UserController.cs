using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PocketClosetWebServiceAPI.Models;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class UserController : Controller, IUserController
    {
        private readonly IConfiguration config;
        public UserController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createUser([FromBody] User user)
        {
            UserDataHandler userDataHandler = new UserDataHandler(config);
            userDataHandler.username = user.username;
            userDataHandler.password = user.password;
            int userId = userDataHandler.createUser();
            user.userId = userId;
            Response response = new Response();
            response.data = JsonConvert.SerializeObject(user);
            response.status = true;
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateUser([FromBody] User user)
        {
            UserDataHandler userDataHandler = new UserDataHandler(config);
            userDataHandler.password = user.password;
            userDataHandler.userId = user.userId;
            bool result = userDataHandler.updateUser();
            Response response = new Response();
            response.status = result;
            return Json(response);
        }
    }
}