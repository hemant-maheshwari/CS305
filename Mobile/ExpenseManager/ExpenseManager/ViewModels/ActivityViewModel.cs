using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseManager.ViewModels
{
    public class ActivityViewModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string transactionTitle { get; set; }
        public double amount{ get; set; }

        public string source
        {
            get
            {
                return firstName + " " + lastName + " Added " + "'" + transactionTitle + "'";
            }
        }
        public string transac
        {
            get {
                return "Amount: " + amount;                
            }
        }
        
    }

    }

