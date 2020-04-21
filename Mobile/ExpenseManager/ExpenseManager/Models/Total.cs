using System;
namespace ExpenseManager.Models
{
    public class Total
    {
        public Total()
        {
        }

        public Total(int userId, double incomeAmount, double expenseAmount)
        {
            this.userId = userId;
            this.incomeAmount = incomeAmount;
            this.expenseAmount = expenseAmount;
        }

        public int userId { get; set; }
        public double incomeAmount { get; set; }
        public double expenseAmount { get; set; }
    }
}
