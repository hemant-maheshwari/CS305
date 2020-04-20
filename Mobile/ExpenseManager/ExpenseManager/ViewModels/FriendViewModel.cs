using System;
namespace ExpenseManager.ViewModels
{
    public class FriendViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int friendId { get; set; }

        public FriendViewModel()
        {
        }
        public string toString()
        {
            return firstName + " " + lastName;
        }

        }
}
