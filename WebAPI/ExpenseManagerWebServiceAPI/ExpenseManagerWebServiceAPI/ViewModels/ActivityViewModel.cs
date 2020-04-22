using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.ViewModels
{
    public class ActivityViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string transactionTitle { get; set; }
        public double amount { get; set; }
    }
}
