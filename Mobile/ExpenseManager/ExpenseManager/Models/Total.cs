using System;
namespace ExpenseManager.Models
{
    public class Total
    {
        public Total()
        {
        }

        public int userId { get; set; }
        public double incomeAmount { get; set; }
        public double expenseAmount { get; set; }
    }
}
