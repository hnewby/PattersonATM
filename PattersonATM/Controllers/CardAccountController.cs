using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PattersonATM.Controllers
{
    [Route("api/CardAccount")]
    public class CardAccountController : Controller
    {
        private readonly ATMDbContext _context;

        public CardAccountController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCardAccount([FromBody] Models.CardAccount cardAccount)
        {
            var result = _context.CardAccounts.Add(cardAccount);
            await _context.SaveChangesAsync();
            return new OkObjectResult(result.Entity);
        }
    }
}

