using System;
using PattersonATM.Models;

namespace PattersonATM.DTO
{
	public class AccountDTO
	{
		public string Name { get; set; }
        public string AccountType { get; set; }
        public decimal Balance { get; set; }
    }
}

