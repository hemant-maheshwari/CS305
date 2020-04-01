using System;
namespace ExpenseManager.Models
{
    public class Account
    {
        public Account()
        {
        }

        public int userId { get; set; } //same as userId
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

    }
}
