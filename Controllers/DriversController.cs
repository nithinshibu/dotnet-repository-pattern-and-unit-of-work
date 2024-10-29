using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternAndUnitOfWork.Core;
using RepositoryPatternAndUnitOfWork.Data;
using RepositoryPatternAndUnitOfWork.Models;

namespace RepositoryPatternAndUnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DriversController(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Drivers.GetAll());    
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var driver = await _unitOfWork.Drivers.GetById(id);
            if(driver == null) return NotFound();
            return Ok(driver);
        }
        
        [HttpPost("AddDriver")]

        public async  Task<IActionResult> AddDriver(Driver driver)
        {
            await _unitOfWork.Drivers.Add(driver);
            await _unitOfWork.CompleteAsync();
            return Ok("Success");
        }

        [HttpDelete("DeleteDriver")]
        public async Task<IActionResult> DeleteDriver(int id) 
        {
            var driver = await _unitOfWork.Drivers.GetById(id);
            if (driver is null)
            {
                return NotFound("Id not found");
            }
            await _unitOfWork.Drivers.Delete(driver);
            await _unitOfWork.CompleteAsync();
            return Ok("Deleted");
        }

        [HttpPatch("UpdateDriver")]

        public async Task<IActionResult> UpdateDriver(Driver driver)
        {
            var existingDriver = await _unitOfWork.Drivers.GetById(driver.Id);
            if (existingDriver is null)
            {
                return NotFound("Id not found");
            }
           await _unitOfWork.Drivers.Update(driver);
           await _unitOfWork.CompleteAsync();
            return Ok("Success");
        }

    }
}
