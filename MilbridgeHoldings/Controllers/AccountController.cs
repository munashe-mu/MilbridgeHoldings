using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public AccountController(ApplicationDbContext context, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager, Microsoft.AspNetCore.Identity.RoleManager<IdentityRole> roleManager, IConfiguration configuration, IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegisterUserRequest user)
        {
            if (user.Email!.ToLower() != user.UserName!.ToLower()) return BadRequest("Email and Username do not match. Your username must be the same with your email address.");
            if (await UserExists(user.Email)) return BadRequest("User already exists");
            var password = PasswordService.GeneratePassword(PasswordOptions.HasCapitals | PasswordOptions.HasDigits | PasswordOptions.HasLower | PasswordOptions.HasSymbols | PasswordOptions.NoRepeating);
            var result = await _userManager.CreateAsync(new ApplicationUser
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.Email,
                EmployeeNumber = user.EmployeeNumber,
                DepartmentId = user.DepartmentId,
                JobTitleId = user.JobTitleId,
                PhoneNumber = user.PhoneNumber,
            }, password);

            if (result.Succeeded)

            {
                await _emailService.SendAsync(new EmailMessage
                {
                    Body = "You have been registered on MilBridge Holdings SA System. Your password is " + password,
                    To = user.Email,
                    Subject = "Milbidge Holdings SA"
                });
                return Ok(user);
            }
            return BadRequest(result);
        }

        private async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email.ToLower());
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationData), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            ApplicationUser user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null) return StatusCode(StatusCodes.Status401Unauthorized, new ErrorResponse(_configuration["MessageResponse:Unauthorised"]));
            IList<string> roles = _userManager.GetRolesAsync(user).Result;
            if (!(user != null && await _userManager.CheckPasswordAsync(user, login.Password))) return Unauthorized();
            IList<string> rolesId = new List<string>();
            foreach (var role in roles)
                rolesId.Add(_roleManager?.Roles?.ToList().FirstOrDefault(a => a.Name.Equals(role))?.Id);
            return Ok(new AuthenticationData
            {
                Token = new ApplicationJwtService(_context, _configuration).JwtTokenBuilder(user, roles),
                UserName = user.UserName,
                UserId = user.Id,
                Roles = roles
            });
        }

        [HttpPost("change-password")]

        public Task<Microsoft.AspNetCore.Identity.IdentityResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            var user = _userManager.FindByIdAsync(changePassword.UserId);
            var result = _userManager.ChangePasswordAsync(user.Result, changePassword.OldPassword, changePassword.NewPassword);
            return result;
        }

        [HttpGet("reset-password/{email}")]
        public Task<Microsoft.AspNetCore.Identity.IdentityResult> ResetPassword([FromRoute] string email)
        {
            var user = _userManager.FindByNameAsync(email);
            var token = _userManager.GeneratePasswordResetTokenAsync(user.Result);
            var password = PasswordService.GeneratePassword(PasswordOptions.HasCapitals |
                           PasswordOptions.HasDigits | PasswordOptions.HasLower |
                           PasswordOptions.HasSymbols | PasswordOptions.NoRepeating);
            var result = _userManager.ResetPasswordAsync(user.Result, token.Result, password);

            if (result.Result.Succeeded)
            {
                _emailService.SendAsync(new EmailMessage
                {
                    Body = "Your password has been reset on Milbridge Holdings SA. Your new password is " + password,
                    To = user.Result.Email,
                    Subject = "Milbridge Holdings SA Account Reset"
                });
            }
            return result;
        }
    }
}
