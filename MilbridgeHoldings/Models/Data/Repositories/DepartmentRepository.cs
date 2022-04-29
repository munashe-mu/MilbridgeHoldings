using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class DepartmentRepository : CrudRepository<Department>
    {
        private readonly ApplicationDbContext _context;
        public DepartmentRepository(ApplicationDbContext context) : base(context) => _context = context;

        public IEnumerable<Department> FindByJobId(int id)
        {
          return _context.Departments.Where(a=>a.Id == id).ToList();
        }

        public async Task<bool> AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            try
            {

                var entry = _context.Departments.First(e => e.Id == department.Id);
                if (entry == null) return false;
                _context.Entry(entry).CurrentValues.SetValues(department);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task FindById(int id)
        {
            _context.JobTitles.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
