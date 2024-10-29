using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryPatternAndUnitOfWork.Models;

namespace RepositoryPatternAndUnitOfWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {

        private static List<Driver> _drivers = new List<Driver>() 
        { 
            new Driver() { Id=1,DriverName="Lewis Hamilton",Team="Mercedes AMG F1",DriverNumber=44 } ,
            new Driver() { Id=2,DriverName="George Russel",Team="Mercedes AMG F1",DriverNumber=63 } ,
            new Driver() { Id=3,DriverName="Sebastian Vettel",Team="Auston Martin",DriverNumber=5 } ,
        
        };

        public DriversController()
        {
                
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_drivers);    
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            return Ok(_drivers.FirstOrDefault(x=>x.Id==id));
        }
        
        [HttpPost("AddDriver")]

        public IActionResult AddDriver(Driver driver)
        {
            _drivers.Add(driver);
            return Ok("Success");
        }

        [HttpDelete("DeleteDriver")]
        public IActionResult DeleteDriver(int id) 
        { 
            var driver = _drivers.FirstOrDefault(x=>x.Id==id);
            if(driver is null)
            {
                return NotFound("Id not found");
            }
            _drivers.Remove(driver);
            return Ok("Deleted");
        }

        [HttpPatch("UpdateDriver")]

        public IActionResult UpdateDriver(Driver driver)
        {
            var driverData = _drivers.FirstOrDefault(x => x.Id == driver.Id);
            if (driverData is null)
            {
                return NotFound("Id not found");
            }
            _drivers.Remove(driverData);
            _drivers.Add(driverData);
            return Ok("Success");
        }

    }
}
