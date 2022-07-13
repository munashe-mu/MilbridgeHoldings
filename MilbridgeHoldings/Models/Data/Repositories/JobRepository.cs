using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly ApplicationDbContext _context;

        public JobRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<JobTitle>> Add(JobTitle request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return new Result<JobTitle>(request);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = await _context.JobTitles.FindAsync(id);
            if (result == null)
                return new Result<bool>(false, new List<string> { "NotFound" });
            _context.JobTitles.Remove(result);
            await _context.SaveChangesAsync();
            return new Result<bool>(true, new List<string> { "Successfully Deleted Data" });
        }

        public async Task<Result<IEnumerable<JobTitle>>> Find()
        {
            var result = await _context.JobTitles.ToListAsync();
            return new Result<IEnumerable<JobTitle>>(result);
        }

        public async Task<Result<IEnumerable<JobTitle>>> FindById(int id)
        {
            var result = await _context.JobTitles.Where(x => x.Id == id).ToListAsync();
            if (result.Count == 0 ) return new Result<IEnumerable<JobTitle>>(false, new List<string> { "Not found" });
            return new Result<IEnumerable<JobTitle>>(result);
        }

        public async Task<Result<JobTitle>> FindJobById(int Id)
        {
            var result = await _context.JobTitles.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) return new Result<JobTitle>(false, new List<string> { "Job Title Not Found" });
            return new Result<JobTitle>(result);
        }

        public async Task<Result<JobTitle>> Update(JobTitle request)
        {
            _context.JobTitles.Update(request);
            await _context.SaveChangesAsync();
            return new Result<JobTitle>(request);
        }
    }
}


