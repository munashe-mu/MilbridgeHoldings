using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public interface IMachinesRepository
    {
        Task<Result<Machines>> Add(Machines request);
        Task<Result<Machines>> Update(Machines request);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<Machines>>> Find();
        Task<Result<IEnumerable<Machines>>> FindById(int id);
        Task<Result<Machines>> FindMachinesById(int Id);
    }
}
