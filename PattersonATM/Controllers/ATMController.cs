using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PattersonATM.DTO;
using PattersonATM.Misc;

namespace PattersonATM.Controllers
{
    [Route("/api/ATM")]
    [ApiController]
    public class ATMController : Controller
    {
        private readonly ATMDbContext _context;

        public ATMController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO request)
        {
            // Remove any dashes or spaces
            request.CardNum = request.CardNum.Replace("-", string.Empty);
            request.CardNum = request.CardNum.Replace(" ", string.Empty);

            if (request.CardNum.Length > 16 || !Regex.IsMatch(request.CardNum, @"\d"))
            {
                return new BadRequestObjectResult(Error.InvalidCardNum);
            }
            else 
            {
                var cardDB = _context.Cards.Where(x => x.CardNumber.Equals(request.CardNum)).FirstOrDefault();
                if (cardDB != null && cardDB.Pin.Equals(request.Pin))
                {
                    //Only get one account. Future design: a card could pull up multiple accounts (checking, savings etc)
                    var cardAccount = _context.CardAccounts.Where(x => x.CardId == cardDB.Id).FirstOrDefault();
                    return new OkObjectResult(cardAccount); //return account or user
                }
                    
            }
            return new NotFoundResult();
        }

    }
}

