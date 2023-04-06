using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PattersonATM.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PattersonATM.Controllers
{
    [Route("/api/Transaction")]
    public class TransactionController : Controller
    {
        private readonly ATMDbContext _context;

        public TransactionController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTransactions([FromQuery]int accountId, [FromQuery]int limit)
        {
            return new OkObjectResult(_context.Transactions.Where(x => x.AccountId == accountId).OrderByDescending(y => y.Date).Take(limit).ToList());
        }
    }
}

