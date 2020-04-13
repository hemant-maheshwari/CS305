using System;
using System.Collections.Generic;
using ExpenseManager.Models;

namespace ExpenseManager.ViewModels
{
    public class TransactionPageModel
    {
        public List<FriendInfo> friendsList { get; set; }
        public Transaction transaction { get; set; }

        public TransactionPageModel()
        {
            friendsList = new List<FriendInfo>();
            transaction = new Transaction();
        }


    }
}
