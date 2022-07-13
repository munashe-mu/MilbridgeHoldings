using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class MachinesRepository : IMachinesRepository
    {
        private readonly ApplicationDbContext _context;
        public MachinesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Machines>> Add(Machines request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return new Result<Machines>(request);       
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = await _context.Machines.FindAsync(id);
            if (result == null) return new Result<bool>(false, new List<string> { "Not Found" });
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return new Result<bool>(true, new List<string> { "Deleted Successfully" });
        }

        public async Task<Result<IEnumerable<Machines>>> Find()
        {
            var result = await _context.Machines.ToListAsync();
            return new Result<IEnumerable<Machines>>(result);
        }

        public async Task<Result<IEnumerable<Machines>>> FindById(int id)
        {
            var result = await _context.Machines.Where(x => x.Id == id).ToListAsync();
            if (result.Count == 0) return new Result<IEnumerable<Machines>>(false, new List<string> { "Not Found"});
            return new Result<IEnumerable<Machines>>(result);
        }

        public async Task<Result<Machines>> FindMachinesById(int Id)
        {
            var result = await _context.Machines.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) return new Result<Machines>(false, new List<string> { "Not Found" });
            return new Result<Machines>(result);
        }

        public async Task<Result<Machines>> Update(Machines request)
        {
            _context.Update(request);
            await _context.SaveChangesAsync();
            return new Result<Machines>(request);
        }
    }
}
