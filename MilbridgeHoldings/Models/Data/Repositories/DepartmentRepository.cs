using Microsoft.EntityFrameworkCore;
using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Department>> Add(Department request)
        {
            await _context.AddAsync(request);
            await _context.SaveChangesAsync();
            return new Result<Department>(request);
        }

        public async Task<Result<bool>> Delete(int id)
        {
            var result = await _context.Departments.FindAsync(id);
            if (result == null) return new Result<bool>(false, new List<string> { "Not Found"});
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return new Result<bool>(true, new List<string> { "Deleted Successfully" });
        }

        public async Task<Result<IEnumerable<Department>>> Find()
        {
            var result = await _context.Departments.ToListAsync();
            return new Result<IEnumerable<Department>>(result);
        }

        public async Task<Result<IEnumerable<Department>>> FindById(int id)
        {
            var result = await _context.Departments.Where(x => x.Id == id).ToListAsync();
            if (result.Count == 0) return new Result<IEnumerable<Department>>(false, new List<string> { "Not Found"});
            return new Result<IEnumerable<Department>>(result);
        }

        public async Task<Result<Department>> FindDeptById(int Id)
        {
            var result = await _context.Departments.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result == null) return new Result<Department>(false, new List<string> { "Not Found" });
            return new Result<Department>(result);
        }

        public async Task<Result<Department>> Update(Department request)
        {
            _context.Update(request);
            await _context.SaveChangesAsync();
            return new Result<Department>(request);
        }
    }
}
