using ExpenseManager.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Controllers
{
    public interface IFriendController
    {
        JsonResult createFriend(Friend friend);
        JsonResult updateFriend(Friend friend);
        JsonResult deleteFriend(int userId2);
        JsonResult getAllFriends(int userId1);
        JsonResult getAllFriendInfo(int userId1);
    }
}
