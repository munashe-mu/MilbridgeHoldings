using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class WorkCentreRepository: IWorkCentreRepository
    {
        private readonly ApplicationDbContext _context;

        public WorkCentreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<WorkCentre>> Add(WorkCentre request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return new Result<WorkCentre>(request);
        }

        public async Task<Result<bool>> Delete(int id)
        {
           var result = await _context.WorkCentres.FindAsync(id);
            if (result == null) 
                return new Result<bool>(false, new List<string> { "Work Centre Not Found"});
            _context.WorkCentres.Remove(result);
            await _context.SaveChangesAsync();
            return new Result<bool>(true, new List<string> { "Deleted Successfully" });
        }

        public async Task<Result<IEnumerable<WorkCentre>>> Find()
        {
            var result = await _context.WorkCentres.ToListAsync();
            return new Result<IEnumerable<WorkCentre>>(result);
        }

        public async Task<Result<IEnumerable<WorkCentre>>> FindById(int id)
        {
            var result = await _context.WorkCentres.Where(x => x.Id == id).ToListAsync();
            if (result.Count == 0) return new Result<IEnumerable<WorkCentre>>(false, new List<string> { "Not Found"});
            return new Result<IEnumerable<WorkCentre>>(result);
        }

        public async Task<Result<WorkCentre>> FindWorkCentreById(int Id)
        {
            var result = await _context.WorkCentres.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) return new Result<WorkCentre>(false, new List<string> { "Not Found" });
            return new Result<WorkCentre>(result);
        }

        public async Task<Result<WorkCentre>> Update(WorkCentre request)
        {
            _context.Update(request);
            await _context.SaveChangesAsync();
            return new Result<WorkCentre>(request);

        }
    }
}
