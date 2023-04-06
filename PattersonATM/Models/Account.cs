using System;
namespace PattersonATM.Models
{
	public class Account
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string AccountNum { get; set; }
		public AccountType Type { get; set; }
		public decimal Balance { get; set; }
		public DateTime CreatedDate { get; set; }

        public enum AccountType
        {
            Checking,
            Savings
        }

    }
	
}

