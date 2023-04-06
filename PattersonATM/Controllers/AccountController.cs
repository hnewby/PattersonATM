using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PattersonATM.DTO;
using PattersonATM.Misc;
using PattersonATM.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PattersonATM.Controllers
{
    [Route("/api/Account")]
    public class AccountController : Controller
    {
        private readonly ATMDbContext _context;

        public AccountController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAccount([FromQuery] int accountId)
        {
            return new OkObjectResult(_context.Accounts.Where(x => x.Id == accountId).FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] AccountDTO account)
        {
            Random generator = new Random();
            var accountNum = generator.Next(0, 1000000).ToString("D6");
            Enum.TryParse(account.AccountType, out Account.AccountType type);
            var result = _context.Accounts.Add(new Account { Name = account.Name, Balance = account.Balance, Type = type, AccountNum = accountNum, CreatedDate = DateTime.Now });
            await _context.SaveChangesAsync();
            return new OkObjectResult(result.Entity);
        }

        [HttpGet("Balance")]
        public IActionResult GetBalance([FromQuery] int accountId)
        {
            var currentAccount = _context.Accounts.Where(x => x.Id == accountId).FirstOrDefault();
            return new OkObjectResult(currentAccount.Balance);
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactDTO transactDTO)
        {
            int userId = _context.Cards.Where(x => x.Id == transactDTO.CardId).Select(y => y.UserId).FirstOrDefault();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            var currentAccount = _context.Accounts.Where(x => x.Id == transactDTO.AccountId).FirstOrDefault();
            currentAccount.Balance += transactDTO.Amount;
            user.TotalTransactionsToday++;
            _context.Accounts.Update(currentAccount);
            _context.Users.Update(user);
            _context.Transactions.Add(new Transaction { AccountId = currentAccount.Id, Date = DateTime.Now, Amount = transactDTO.Amount, Type = TransactionType.Deposit });
            await _context.SaveChangesAsync();
            return new OkResult();
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactDTO transactDTO)
        {
            int userId = _context.Cards.Where(x => x.Id == transactDTO.CardId).Select(y => y.UserId).FirstOrDefault();
            var user = _context.Users.Where(x => x.Id == userId).FirstOrDefault();
            if (user.TotalTransactionsToday >= 10)
            {
                return new BadRequestObjectResult(Error.TransactionLimit);
            }
            if (transactDTO.Amount > 1000)
            {
                return new BadRequestObjectResult(Error.WithdrawLimit);
            }
            var currentAccount = _context.Accounts.Where(x => x.Id == transactDTO.AccountId).FirstOrDefault();
            currentAccount.Balance -= transactDTO.Amount;
            user.TotalTransactionsToday++;
            _context.Accounts.Update(currentAccount);
            _context.Users.Update(user);
            _context.Transactions.Add(new Transaction { AccountId = currentAccount.Id, Date = DateTime.Now, Amount = transactDTO.Amount, Type = TransactionType.Withdraw });
            await _context.SaveChangesAsync();
            return new OkResult();

        }
    }
}

