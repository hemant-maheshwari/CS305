using System;
namespace ExpenseManager.Models
{
    public class ProfilePicture
    {
        public ProfilePicture()
        {
        }

        public int userId { get; set; }//same as user id
        public string profilePicture { get; set; }

    }
}
