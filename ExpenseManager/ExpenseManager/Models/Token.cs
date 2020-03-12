using System;
namespace ExpenseManager.Models
{
    public class Token
    {
        public int id { get; set; }
        public string accessToken { get; set; }
        public string errorDescription { get; set; }
        public DateTime expireDate { get; set; }
        public Token()
        {
        }
    }
}
