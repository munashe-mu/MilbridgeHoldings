using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public class MachinesRepository : CrudRepository<Machines>
    {
        private readonly ApplicationDbContext _context;
        public MachinesRepository(ApplicationDbContext context) : base(context) => _context = context;

        public IEnumerable<Machines> FindByMachineId(int id)
        {
            return _context.Machines.Where(a => a.Id == id).ToList();
        }

        public async Task<bool> AddMachine(Machines machines)
        {
            await _context.Machines.AddAsync(machines);
            await _context.SaveChangesAsync();
            return true; 
        }

        public async Task<bool> UpdateAsync(Machines machines)
        {

            try
            {

                var entry = _context.Machines.First(e => e.Id == machines.Id);
                if (entry == null) return false;
                _context.Entry(entry).CurrentValues.SetValues(machines);
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
            _context.Machines.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
