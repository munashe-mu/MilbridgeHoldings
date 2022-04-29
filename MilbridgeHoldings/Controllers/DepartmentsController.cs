using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilbridgeHoldings.Data;
using MilbridgeHoldings.Models.Data;
using MilbridgeHoldings.Models.Data.Local;
using MilbridgeHoldings.Models.Data.Repositories;

namespace MilbridgeHoldings.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentsController(ApplicationDbContext context, DepartmentRepository departmentRepository)
        {
            _context = context;
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public IEnumerable<Department> Get() => new DepartmentRepository(_context).Find();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IEnumerable<Department> GetByJobTitleById(int id) => new DepartmentRepository(_context).FindByJobId(id);

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post([FromBody] DepartmentRequest request)
        {
            var dept = await new DepartmentRepository(_context).AddDepartment(new Department
            {
                Name = request.Name,
            });

            return StatusCode(StatusCodes.Status200OK, dept);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update([FromBody] Department req)
        {

            var result = await _departmentRepository.UpdateAsync(new Department
            {
                Id = req.Id,
                Name = req.Name,
            });
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult DeleteTask([FromRoute] int id)
        {
            try
            {
                var repo = new DepartmentRepository(_context);
                var deptToDelete = repo.FindById(id);
                if (deptToDelete == null) return NotFound($"Dept with Id = {id} not found");
                repo.Delete(id);
                return Ok("Deleted Successfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error deleting data");
            }
        }
    }
}
