using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PattersonATM.Models;

namespace PattersonATM
{
	public class ATMDbContext : DbContext
	{

        public ATMDbContext(
            DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ATMDb");
        }
        public DbSet<Account> Accounts { get; set; }
		public DbSet<Card> Cards { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<CardAccount> CardAccounts { get; set; }
		public DbSet<User> Users { get; set; }
	}
}

