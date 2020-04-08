using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseManager.Models;
using ExpenseManagerWebServiceAPI.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PocketClosetWebServiceAPI.Models;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    [Route("v1/api/[controller]")]
    public class ProfilePictureController : Controller, IProfilePictureController
    {
        private readonly IConfiguration config;
        public ProfilePictureController(IConfiguration config) {
            this.config = config;
        }

        [Route("create")]
        [HttpPost]
        public JsonResult createProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            return saveProfilePicture(profilePicture, "create_profile_picture");
        }

        [Route("delete/{userId}")]
        [HttpGet]
        public JsonResult deleteProfilePicture(int userId)
        {
            throw new NotImplementedException();
        }

        [Route("get/{userId}")]
        [HttpGet]
        public JsonResult getProfilePicture(int userId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("update")]
        [HttpPost]
        public JsonResult updateProfilePicture([FromBody] ProfilePicture profilePicture)
        {
            return saveProfilePicture(profilePicture, "update_profile_picture");
        }

        private JsonResult saveProfilePicture(ProfilePicture profilePicture, string command) {
            Response response = new Response();
            ProfilePictureDataHandler profilePictureDataHandler = new ProfilePictureDataHandler(config);
            //main code
            return Json(response);
        }
    }
}