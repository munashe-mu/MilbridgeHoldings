using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class WorkCentreRepository: CrudRepository<WorkCentre>
    {
        private readonly ApplicationDbContext _context;
        public WorkCentreRepository(ApplicationDbContext context) : base(context) => _context = context;

        public IEnumerable<WorkCentre> FindByWorkCentreId(int id)
        {
            return _context.WorkCentres.Where(a => a.Id == id).ToList();
        }

        public async Task<bool> AddWorkCentre(WorkCentre workCentre)
        {
            await _context.WorkCentres.AddAsync(workCentre);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(WorkCentre workCentre)
        {
            try
            {

                var entry = _context.WorkCentres.First(e => e.Id == workCentre.Id);
                if (entry == null) return false;
                _context.Entry(entry).CurrentValues.SetValues(workCentre);
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
           _context.WorkCentres.Where(a=> a.Id == id).ToList(); 
        }
    }
}
