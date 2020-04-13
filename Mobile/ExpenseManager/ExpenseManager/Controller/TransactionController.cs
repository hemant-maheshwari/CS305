using ExpenseManager.Models;
using ExpenseManager.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace ExpenseManager.Controller
{
    public class TransactionController:BaseController<Transaction>
    {
        private RestAPIService restAPIService;

        public TransactionController()
        {
            restAPIService = new RestAPIService();
        }
        //CRUD function - RestAPICRUDService
        //create, update, delete will return bool
        //get return object
        //getAll return List<object>
        //transaction/get/{transid}- GET
        //trascation/create - POST



    }
}
