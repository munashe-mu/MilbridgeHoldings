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
        private readonly IMachinesRepository _machinesRepository;
        public MachinesController(IMachinesRepository machinesRepository)
        {
            _machinesRepository = machinesRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _machinesRepository.Find();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMachinesById(int id)
        {
            var result = await _machinesRepository.FindMachinesById(id);
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpGet("getMachinesById/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _machinesRepository.FindById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] MachinesReq request)
        {
            var job = await _machinesRepository.Add (new Machines
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
            var result = await _machinesRepository.Update(new Machines
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
       public async Task<IActionResult> Delete(int id) => Ok(await _machinesRepository.Delete(id));
    }
}
