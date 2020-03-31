using System;
namespace ExpenseManager.Models
{
    public class ProfilePicture
    {
        public ProfilePicture()
        {
        }

        public int profilePictureId { get; set; }//same as user id
        public string profilePicture { get; set; }

    }
}
