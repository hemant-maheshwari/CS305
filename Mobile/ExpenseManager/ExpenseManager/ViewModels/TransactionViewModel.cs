using System;
using System.Collections.Generic;
using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class TransactionViewModel
    {
        public List<FriendViewModel> friendsList { get; set; }
        public Transaction transaction { get; set; }

        public TransactionViewModel()
        {
            friendsList = new List<FriendViewModel>();
            transaction = new Transaction();
        }

        public List<string> friendsListToString() {
            List<string> friendListString = new List<string>();
            for (int i =0; i < friendsList.Count; i++) {
                friendListString.Add(friendsList[i].toString());
            }
            return friendListString;
        }
    }
}
