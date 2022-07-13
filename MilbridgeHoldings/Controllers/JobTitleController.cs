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
    public class JobTitleController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
      
        public JobTitleController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetJobTitle()
        {
            var result = await _jobRepository.Find();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByJobTitleById(int id)
        {
            var result = await _jobRepository.FindById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpGet("getjobsById/{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByJobTitlesById(int id)
        {
            var result = await _jobRepository.FindJobById(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post([FromBody] JobTitleRequest request )
        {
            var job = await _jobRepository.Add(new JobTitle
            {
                Name = request.Name,
            });
            return StatusCode(StatusCodes.Status200OK, job);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] JobTitle req)
        {
   
            var result = await _jobRepository.Update(new JobTitle
            {
                Id = req.Id,
                Name = req.Name,
            });
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id) => Ok(await _jobRepository.Delete(id));

    }
}
