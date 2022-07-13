using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public interface IWorkCentreRepository
    {
        Task<Result<WorkCentre>> Add(WorkCentre request);
        Task<Result<WorkCentre>> Update(WorkCentre request);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<WorkCentre>>> Find();
        Task<Result<IEnumerable<WorkCentre>>> FindById(int id);
        Task<Result<WorkCentre>> FindWorkCentreById(int Id);
    }
}
