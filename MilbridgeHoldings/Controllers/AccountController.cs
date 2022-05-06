using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MilbridgeHoldings.Models.Data;
using MilbridgeHoldings.Models.Data.Local;
using MilbridgeHoldings.Services;
using ModelLibrary.Services;
using PasswordOptions = MilbridgeHoldings.Services.PasswordOptions;

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
            var password = PasswordService.GeneratePassword(PasswordOptions.HasCapitals | PasswordOptions.HasDigits | PasswordOptions.HasLower |
                  PasswordOptions.HasSymbols | PasswordOptions.NoRepeating);
            var result = await _userManager.CreateAsync(new Employee
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                EmployeeNumber = user.EmployeeNumber,
                DepartmentId = user.DepartmentId,
                JobTitleId = user.JobTitleId,
            },password);
            return Ok(result);
        }
    }
}
