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
        private RestAPICRUDService<User> RestAPICRUDService;

        public UserController() {
            restAPIService = new RestAPIService();
            RestAPICRUDService = new RestAPICRUDService<User>();
        }

        //CRUD function - RestAPICRUDService
        //create, update, delete will return bool
        //get return object
        //getAll return List<object>
        public async Task<bool> createUser(User user) {
            return await RestAPICRUDService.createModelAsync(user);
        }

        //user/check/{username}- GET
        //user/create - POST
        public async Task<bool> checkUsername(string username)
        {
            return await restAPIService.checkUsernameAsync(username);
        }

        //public async Task<User> getUser(int userId);
    }
}
