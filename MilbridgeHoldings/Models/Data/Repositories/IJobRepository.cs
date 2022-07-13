using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public interface IJobRepository
    {
        Task<Result<JobTitle>> Add(JobTitle request);
        Task<Result<JobTitle>> Update(JobTitle request);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<JobTitle>>> Find();
        Task<Result<IEnumerable<JobTitle>>> FindById(int id);
        Task<Result<JobTitle>> FindJobById(int Id);
    }
}
