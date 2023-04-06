using System;
namespace PattersonATM.DTO
{
	public class TransactDTO
	{
		public int AccountId { get; set; }
		public decimal Amount { get; set; }
		public int CardId { get; set; }
	}
}

