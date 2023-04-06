using System;
namespace PattersonATM
{
	public class AppData
	{
		public int CurrentCardId { get; set; }
		public int CurrentAccountId { get; set; }
		public HttpClient Client { get; set; }
	}
}

