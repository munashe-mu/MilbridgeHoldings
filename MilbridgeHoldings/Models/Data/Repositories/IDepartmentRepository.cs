using MilbridgeHoldings.Data;

namespace MilbridgeHoldings.Models.Data.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Result<Department>> Add(Department request);
        Task<Result<Department>> Update(Department request);
        Task<Result<bool>> Delete(int id);
        Task<Result<IEnumerable<Department>>> Find();
        Task<Result<IEnumerable<Department>>> FindById(int id);
        Task<Result<Department>> FindDeptById(int Id);
    }
}
