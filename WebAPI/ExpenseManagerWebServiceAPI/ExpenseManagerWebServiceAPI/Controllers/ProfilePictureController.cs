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
    public class ProfilePictureController : Controller, IProfilePictureController
    {
        private readonly IConfiguration config;
        public ProfilePictureController(IConfiguration config) {
            this.config = config;
        }
        public JsonResult createProfilePicture(ProfilePicture profilePicture)
        {
            throw new NotImplementedException();
        }

        public JsonResult deleteProfilePicture(int userId)
        {
            throw new NotImplementedException();
        }

        public JsonResult getProfilePicture(int userId)
        {
            throw new NotImplementedException();
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult updateProfilePicture(ProfilePicture profilePicture)
        {
            throw new NotImplementedException();
        }
    }
}