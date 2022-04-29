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
    public class MachinesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly MachinesRepository _machinesRepository;
        public MachinesController(ApplicationDbContext context, MachinesRepository machinesRepository)
        {
            _context = context;
            _machinesRepository = machinesRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public IEnumerable<Machines> Get() => new MachinesRepository(_context).Find();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IEnumerable<Machines> GetByJobTitleById(int id) => new MachinesRepository(_context).FindByMachineId(id);

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post([FromBody] MachinesReq request)
        {
            var job = await new MachinesRepository(_context).AddMachine(new Machines
            {
                Name = request.Name,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                HoursWorked = request.HoursWorked,
                WorkCentreId = request.WorkCentreId,
            });

            return StatusCode(StatusCodes.Status200OK, job);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Update([FromBody] MachinesReq req)
        {

            var result = await _machinesRepository.UpdateAsync(new Machines
            {
                Name = req.Name,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now,
                WorkCentreId= req.WorkCentreId,
                HoursWorked= req.HoursWorked,
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
                var repo = new MachinesRepository(_context);
                var machineToDelete = repo.FindById(id);
                if (machineToDelete == null) return NotFound($"Task with Id = {id} not found");
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
