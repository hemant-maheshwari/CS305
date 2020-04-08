using System;
namespace ExpenseManager.Models
{
    public class User
    {
        public User() { }
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }

    }
}
