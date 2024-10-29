using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternAndUnitOfWork.Data;
using RepositoryPatternAndUnitOfWork.Models;

namespace RepositoryPatternAndUnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public DriversController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Drivers.ToListAsync());    
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if(driver == null) return NotFound();
            return Ok(driver);
        }
        
        [HttpPost("AddDriver")]

        public async  Task<IActionResult> AddDriver(Driver driver)
        {
            _dbContext.Drivers.Add(driver);
            await _dbContext.SaveChangesAsync();
            return Ok("Success");
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> DeleteDriver(int id) 
        { 
            var driver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == id);
            if(driver is null)
            {
                return NotFound("Id not found");
            }
            _dbContext.Drivers.Remove(driver);
            await _dbContext.SaveChangesAsync();
            return Ok("Deleted");
        }

        [HttpPatch("UpdateDriver")]

        public async Task<IActionResult> UpdateDriver(Driver driver)
        {
            var existingDriver = await _dbContext.Drivers.FirstOrDefaultAsync(x => x.Id == driver.Id);
            if (existingDriver is null)
            {
                return NotFound("Id not found");
            }
            existingDriver.DriverName= driver.DriverName;
            existingDriver.DriverNumber= driver.DriverNumber;
            existingDriver.Team = driver.Team;
            _dbContext.Drivers.Update(existingDriver);
            await _dbContext.SaveChangesAsync();
            return Ok("Success");
        }

    }
}
