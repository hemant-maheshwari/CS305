using System;
namespace ExpenseManager.Models
{
    public class User
    {

        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public User(){}
        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }


        public bool checkInformation()
        {
            if(!this.username.Equals("") && !this.password.Equals("")){
                return true;
            }else{
                return false;
            }
        }
    }
}
