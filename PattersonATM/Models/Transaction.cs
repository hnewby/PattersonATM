using System;
namespace PattersonATM.Models
{
	public class Transaction
	{
		public int Id { get; set; }
		public int AccountId { get; set; }
		public DateTime Date { get; set; }
		public TransactionType Type { get; set; }
		public decimal Amount { get; set; }
	}

	public enum TransactionType
	{
		Deposit,
		Withdraw,
		Transfer
	}
}

