using MilbridgeHoldings.Data;


namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class JobRepository: CrudRepository<JobTitle>
    {
        private readonly ApplicationDbContext _context;
        public JobRepository(ApplicationDbContext context) : base(context) => _context = context;

        public async Task<bool> AddJobTitle(JobTitle jobTitle)
        {
            await _context.JobTitles.AddAsync(jobTitle);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(JobTitle jobTitle)
        {
            try
            {

                var entry = _context.JobTitles.First(e => e.Id == jobTitle.Id);
                if (entry == null) return false;
                _context.Entry(entry).CurrentValues.SetValues(jobTitle);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<JobTitle> FindByJobId(int id)
        {
            return _context.JobTitles.Where(a => a.Id.Equals(id)).ToList();
        }

        public async Task FindById(int id)
        {
           _context.JobTitles.Where(a=>a.Id == id).FirstOrDefault();
        }

        public async Task  Delete(int id)
        {
           _context.Remove(id);
        }
    }
}


