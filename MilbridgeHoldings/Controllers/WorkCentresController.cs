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
    public class WorkCentresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly WorkCentreRepository _workCentreRepository;
        public WorkCentresController(ApplicationDbContext context, WorkCentreRepository workCentreRepository)
        {
            _context = context;
            _workCentreRepository = workCentreRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public IEnumerable<WorkCentre> Get() => new WorkCentreRepository(_context).Find();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IEnumerable<WorkCentre> GetByWorkCentreById(int id) => new WorkCentreRepository(_context).FindByWorkCentreId(id);

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post([FromBody] WorkCentreReq request)
        {
            var job = await new WorkCentreRepository(_context).AddWorkCentre(new WorkCentre
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
            });
            return StatusCode(StatusCodes.Status200OK, job);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update([FromBody] WorkCentreReq req)
        {

            var result = await _workCentreRepository.UpdateAsync(new WorkCentre
            {
                Name = req.Name,
                DepartmentId = req.DepartmentId,
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
                var repo = new WorkCentreRepository(_context);
                var workCentre = repo.FindById(id);
                if (workCentre == null) return NotFound($"Task with Id = {id} not found");
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
