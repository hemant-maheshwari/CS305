using System;
namespace ExpenseManager.Models
{
    public class ProfilePicture
    {
        public ProfilePicture()
        {
        }

        public int userId { get; set; }//same as user id base64string x02Ab45
        public string profilePicture { get; set; }

    }
}
