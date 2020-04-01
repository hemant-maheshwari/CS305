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
    public class FriendController : Controller, IFriendController
    {
        private readonly IConfiguration config;

        public FriendController(IConfiguration config) {
            this.config = config;
        }
        public JsonResult createFriend(Friend friend)
        {
            throw new NotImplementedException();
        }

        public JsonResult deleteFriend(int userId2)
        {
            throw new NotImplementedException();
        }

        public JsonResult getAllFriends(int userId1)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateFriend(Friend friend)
        {
            throw new NotImplementedException();
        }
    }
}