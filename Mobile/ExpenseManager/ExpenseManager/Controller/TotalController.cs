using System;
using System.Threading.Tasks;
using ExpenseManager.Models;
using ExpenseManager.Service;

namespace ExpenseManager.Controller
{
    public class TotalController : BaseController<Total>
    {
        private RestAPIService restAPIService;

        public TotalController()
        {
            restAPIService = new RestAPIService();
        }
    }
}
