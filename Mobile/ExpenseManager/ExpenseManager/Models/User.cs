using System;
namespace ExpenseManager.Models
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public User() { }
        public User(string username, string password, string firstName, string lastName, string email, string phone)
        {
            this.username = username;
            this.password = password;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
        }
    }
}