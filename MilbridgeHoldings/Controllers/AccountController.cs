using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MilbridgeHoldings.Models.Data;
using MilbridgeHoldings.Models.Data.Local;

namespace MilbridgeHoldings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(ApplicationDbContext context, UserManager<Employee> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest user)
        {

        }
    }
}
