using Microsoft.EntityFrameworkCore;
using RepositoryPatternAndUnitOfWork.Data;
using RepositoryPatternAndUnitOfWork.Models;

namespace RepositoryPatternAndUnitOfWork.Core.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Driver>> GetAll()
        {
            try
            {
                return await _context.Drivers.Where(x => x.Id < 100).ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Driver?> GetByDriverNumber(int driverNumber)
        {
            try
            {
                return await _context.Drivers.FirstOrDefaultAsync(x => x.DriverNumber == driverNumber);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
