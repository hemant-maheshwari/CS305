using System;
using System.Collections.Generic;
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
    public class FriendController : Controller, IFriendController
    {
        private readonly IConfiguration config;

        public FriendController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createFriend([FromBody] Friend friend)
        {
            return saveFriend(friend, "create");
        }

        [Route("delete/{userId2}")]
        [HttpGet]
        public JsonResult deleteFriend(int userId2)
        {
            Response response = new Response();
            try {
                FriendDataHandler friendDataHandler = new FriendDataHandler(config);
                bool result = friendDataHandler.deleteFriend(userId2);
                response.status = result;
            } catch(Exception ex) {
                response.message = ex.Message;
                response.status = false;                
            }
            return Json(response);
        }

        [Route("getAllFriendInfo/{userId1}")]
        [HttpGet]
        public JsonResult getAllFriendInfo(int userId1)
        {
            Response response = new Response();
            try
            {
                FriendDataHandler friendDataHandler = new FriendDataHandler(config);
                List<FriendInfo> friends = friendDataHandler.getAllFriendInfo(userId1);
                response.status = true;
                response.data = JsonConvert.SerializeObject(friends);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.status = false;
            }
            return Json(response);
        }

        [Route("getAll/{userId1}")]
        [HttpGet]
        public JsonResult getAllFriends(int userId1)
        {
            Response response = new Response();
            try
            {
                FriendDataHandler friendDataHandler = new FriendDataHandler(config);
                List<Friend> friends = friendDataHandler.getAllFriends(userId1);
                response.status = true;
                response.data = JsonConvert.SerializeObject(friends);
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
                response.status = false;
            }
            return Json(response);
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateFriend([FromBody] Friend friend)
        {
            return saveFriend(friend, "update");
        }

        private JsonResult saveFriend(Friend friend, string command) {
            bool result = false;
            FriendDataHandler friendDataHandler = new FriendDataHandler(config);
            friendDataHandler.userId1 = friend.userId1;
            friendDataHandler.userId2 = friend.userId2;
            friendDataHandler.amount = friend.amount;
            if (command.Equals("create")) {
                result = friendDataHandler.createFriend();
            }
            if (command.Equals("update")) {
                result = friendDataHandler.updateFriend();
            }            
            Response response = new Response();
            response.status = result;
            return Json(response);
        }

    }
}