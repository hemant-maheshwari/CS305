using System;
namespace ExpenseManager.Models
{
    public class Friend
    {
        public Friend(){}

        public Friend(int userId1, int userId2, double amount)
        {
            this.userId1 = userId1;
            this.userId2 = userId2;
            this.amount = amount;
        }

        public int userId1 { get; set; } // user who creates transaction
        public int userId2 { get; set; } // user who is involved in transaction (the friend)
        public double amount { get; set; } // negative if userId1 owes userId2, positive if userId2 owes userId1
    }
}
