using ExpenseManager.Models;
using ExpenseManager.Service;
using ExpenseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ExpenseManager.Controller
{
    public class FriendController : BaseController<Friend>
    {
        private RestAPIService restAPIService;
        public FriendController()
        {
            restAPIService = new RestAPIService();
        }

        public async Task<List<FriendViewModel>> getAllFriendsInfo(int userId) //calling on rest api to return a list of Friend View Model
        {
            return await restAPIService.getAllFriendsInfoAsync(userId);
        }
    }
}
