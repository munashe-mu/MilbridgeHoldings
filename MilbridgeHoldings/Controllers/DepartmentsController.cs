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
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _departmentRepository.Find();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetDeptById(int id)
        {
            var result = await _departmentRepository.FindDeptById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("getDeptById/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByDeptsById(int id)
        {
            var result = await _departmentRepository.FindById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] DepartmentRequest request)
        {
            var dept = await _departmentRepository.Add(new Department
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
            var result = await _departmentRepository.Update(new Department
            {
                Id = req.Id,
                Name = req.Name,
            });
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
       public async Task<IActionResult> Delete(int id)=> Ok(await _departmentRepository.Delete(id));    
    }
}
