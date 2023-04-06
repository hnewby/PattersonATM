using System;
namespace PattersonATM.Misc
{
	public class Error : Exception
	{

		public string Message { get; set; }

        public static Error InvalidCardNum => new Error { Message = "Please enter a valid card number" };
        public static Error InvalidPin => new Error { Message = "Please enter a valid pin" };
        public static Error TransactionLimit => new Error { Message = "Transaction limit reached" };
        public static Error WithdrawLimit => new Error { Message = "Over withdraw limit" };
    }
}

