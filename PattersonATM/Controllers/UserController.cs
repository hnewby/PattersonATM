using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PattersonATM.DTO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PattersonATM.Controllers
{
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly ATMDbContext _context;

        public UserController(ATMDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserDTO user)
        {
            var result = _context.Users.Add(new Models.User { Name = user.Name, MobileNum = user.MobileNum, TotalTransactionsToday = 0 });
            await _context.SaveChangesAsync();
            return new OkObjectResult(result.Entity);
        }
    }
}

