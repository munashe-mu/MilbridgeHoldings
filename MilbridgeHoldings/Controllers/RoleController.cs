using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MilbridgeHoldings.Models.Data;

namespace MilbridgeHoldings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet("{id}")]
        public async Task<IdentityRole> GetRole([FromRoute] string id) => await _roleManager.FindByIdAsync(id);

        [HttpGet]
        public IQueryable<IdentityRole> GetRoles() => _roleManager.Roles;

        [HttpPost("Post-Role")]
        public async Task<IActionResult> PostRole([FromBody] Role role)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole { Name = role.Name});
            return Ok(new Models.Local.ActionResult<string>(result.Succeeded, result.Succeeded ? "Application User Role Posted Successfully" : result.Errors.FirstOrDefault()!.Description));
        }


        [HttpPut("Update-Role/{id}")]
        public async Task<IActionResult> UpdateRole([FromRoute]string id,[FromBody] Role role)
        {
            var roles = await _roleManager.FindByIdAsync(id);
            if (roles == null) return StatusCode(StatusCodes.Status404NotFound);
            roles.Name = role.Name;
            var result = await _roleManager.UpdateAsync(roles);
            return Ok(new Models.Local.ActionResult<string>(result.Succeeded, result.Succeeded ? "Application User Role Updated Successfully" : result.Errors.FirstOrDefault()!.Description));

        }

        [HttpDelete("Delete-Role/{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
            var roles = await _roleManager.FindByIdAsync(id);
            if (roles == null) return StatusCode(StatusCodes.Status404NotFound);
            var result = await _roleManager.DeleteAsync(roles);
            return Ok( new Models.Local.ActionResult<string>(result.Succeeded, result.Succeeded ? "Application User Role Deleted Successfully" : result.Errors.FirstOrDefault()!.Description));
        }
    }
}
