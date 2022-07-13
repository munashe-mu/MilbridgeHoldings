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
        private readonly IWorkCentreRepository _workCentreRepository;

        public WorkCentresController(IWorkCentreRepository workCentreRepository)
        {
            _workCentreRepository = workCentreRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _workCentreRepository.Find();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByWorkCentreById(int id)
        {
            var result = await _workCentreRepository.FindById(id);
            return StatusCode(StatusCodes.Status200OK,result);
        }

        [HttpGet("getWorkCentresById/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByWorkCentresById(int id)
        {
            var result = await _workCentreRepository.FindWorkCentreById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] WorkCentreReq request)
        {
            var result = await _workCentreRepository.Add(new WorkCentre
            {
                Name = request.Name,
                DepartmentId = request.DepartmentId,
            });
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] WorkCentreReq req)
        {

            var result = await _workCentreRepository.Update(new WorkCentre
            {
                Name = req.Name,
                DepartmentId = req.DepartmentId,
            });
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) => Ok(await _workCentreRepository.Delete(id));

    }
}
