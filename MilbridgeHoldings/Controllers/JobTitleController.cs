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
        private readonly ApplicationDbContext _context;
        private readonly JobRepository _jobRepository;
        public JobTitleController(ApplicationDbContext context, JobRepository jobRepository)
        {
            _context = context;
            _jobRepository = jobRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        public IEnumerable<JobTitle> Get() => new JobRepository(_context).Find();

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IEnumerable<JobTitle> GetByJobTitleById(int id) => new JobRepository(_context).FindByJobId(id);

        [HttpPost]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]

        public async Task<IActionResult> Post([FromBody] JobTitleRequest request )
        {
            var job = await new JobRepository(_context).AddJobTitle(new JobTitle
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
   
            var result = await _jobRepository.UpdateAsync(new JobTitle
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
                var repo = new JobRepository(_context);
                var jobToDelete = repo.FindById(id);
                if (jobToDelete == null) return NotFound($"Task with Id = {id} not found");
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
