using ExpenseManager.Models;
using ExpenseManager.Service;
using ExpenseManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ExpenseManager.Controller
{
    public class TransactionController : BaseController<Transaction>
    {
        private RestAPIService restAPIService;

        public TransactionController()
        {
            restAPIService = new RestAPIService();
        }

        public async Task<List<ActivityViewModel>> getAllActivity(int userId) {  // calling on rest api to return a list of activity view model
            return await restAPIService.getAllActivityAsnyc(userId);
        }

        //CRUD function - RestAPICRUDService
        //create, update, delete will return bool
        //get return object
        //getAll return List<object>
        //transaction/get/{transid}- GET
        //trascation/create - POST
    }
}
