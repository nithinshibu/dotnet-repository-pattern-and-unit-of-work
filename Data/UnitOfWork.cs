using RepositoryPatternAndUnitOfWork.Core;
using RepositoryPatternAndUnitOfWork.Core.Repositories;

namespace RepositoryPatternAndUnitOfWork.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        //private readonly ILogger _logger;
        public IDriverRepository Drivers { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var _logger = loggerFactory.CreateLogger("logs");
            Drivers = new DriverRepository(context,_logger);   
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
           _context.Dispose();
        }
    }
}
