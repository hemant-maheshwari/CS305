using System;
namespace ExpenseManager.Models
{
    public class Account
    {
        public Account()
        {
        }

        public Account(int userId, string firstName, string lastName, string email, string phone) {
            this.userId = userId;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
        }

        public int userId { get; set; } //same as userId
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

    }
}
