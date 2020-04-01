using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface IProfilePictureController
    {
        JsonResult createProfilePicture(ProfilePicture profilePicture);
        JsonResult updateProfilePicture(ProfilePicture profilePicture);
        JsonResult deleteProfilePicture(int userId);
        JsonResult getProfilePicture(int userId);
    }
}
