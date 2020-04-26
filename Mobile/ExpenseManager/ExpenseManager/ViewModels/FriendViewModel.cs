using System;
namespace ExpenseManager.ViewModels
{
    public class FriendViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int friendId { get; set; }
        public double amount { get; set; }

        public string owedAmount {
            get {
                if (amount < 0)
                {
                    return "You owe " + (-1 * amount);
                }
                else {
                    return "You get " + amount;
                }
            }
        }

        public FriendViewModel(){}

        public string toString()
        {
            return firstName + " " + lastName;
        }

    }
}
