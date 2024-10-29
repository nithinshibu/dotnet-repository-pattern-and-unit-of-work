using RepositoryPatternAndUnitOfWork.Models;

namespace RepositoryPatternAndUnitOfWork.Core
{
    public interface IDriverRepository:IGenericRepository<Driver>
    {
        Task<Driver?> GetByDriverNumber(int driverNumber);

    }
}
