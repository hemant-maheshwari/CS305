using System;
namespace ExpenseManagerWebServiceAPI.ViewModels
{
    public class FriendInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int friendId { get; set; }

        public double amount { get; set; }

        public FriendInfo()
        {
        }
    }
}
