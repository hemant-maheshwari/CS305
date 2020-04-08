using ExpenseManager.Models;
using ExpenseManager.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Controller
{
    public class UserController
    {
        private RestAPIService restAPIService;

        public UserController() {
            restAPIService = new RestAPIService();
        }

        public async Task<bool> createUser(User user) {           
            return await restAPIService.createUserAsync(user);
        }

        //public async Task<User> getUser(int userId);
    }
}
