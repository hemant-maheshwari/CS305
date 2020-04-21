using System;
using System.Collections.Generic;

namespace ExpenseManager.Models
{
    public class Transaction
    {
        public Transaction()
        {
        }

        public int transactionId { get; set;}
        public int userId { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public double amount { get; set; }
        //public List<string> friends { get; set; }
        //public string friend { get; set; }
        public string transactionPicture { get; set; } //photo of receipt
        public string dateCreated { get; set; }
        public int friendId { get; set; }
        public string dateUpdated { get; set; }

        public Transaction(int userId, string title, string type, double amount, int friendId, string transactionPicture, string dateCreated)
        {
            this.userId = userId;
            this.title = title;
            this.type = type;
            this.amount = amount;
            this.friendId = friendId;
            this.transactionPicture = transactionPicture;
            this.dateCreated = dateCreated;
        }
    }
}
