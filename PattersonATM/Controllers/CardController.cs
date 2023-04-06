using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PattersonATM.DTO;
using PattersonATM.Misc;
using PattersonATM.Models;

namespace PattersonATM.Controllers
{
    [Route("/api/Card")]
    public class CardController : Controller
    {
        private readonly ATMDbContext _context;

        public CardController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostCard ([FromBody] NewCardRequest request)
        {
            // Remove any dashes or spaces
            request.CardNum = request.CardNum.Replace("-", string.Empty);
            request.CardNum = request.CardNum.Replace(" ", string.Empty);

            if (request.CardNum.Length != 16 || !Regex.IsMatch(request.CardNum, @"\d"))
            {
                return new BadRequestObjectResult(Error.InvalidCardNum);
            }
            if (request.Pin.Length != 4 || !Regex.IsMatch(request.Pin, @"\d"))
            {
                return new BadRequestObjectResult(Error.InvalidPin);
            }
            if(_context.Cards.Where(x => x.CardNumber.Equals(request.CardNum)).Count() > 0)
            {
                return new BadRequestObjectResult(Error.InvalidCardNum);
            }
            var result = _context.Cards.Add(new Card { CardNumber = request.CardNum, Pin = request.Pin, UserId = request.UserId });
            await _context.SaveChangesAsync();

            return new OkObjectResult(result.Entity);
        }
    }
}

