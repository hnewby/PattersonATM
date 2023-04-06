using System;
namespace PattersonATM.Models
{
	public class Card
	{
		public int Id { get; set; }
		public string CardNumber { get; set; }
		public string Pin { get; set; }
		public int UserId { get; set; }
	}
}

