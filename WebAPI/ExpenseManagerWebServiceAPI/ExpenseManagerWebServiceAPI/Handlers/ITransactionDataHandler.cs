using ExpenseManagerWebServiceAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseManagerWebServiceAPI.Handlers
{
    public interface ITransactionDataHandler
    {
        bool createTransaction();

        List<ActivityViewModel> getAllActivity(int userId);
    }
}
