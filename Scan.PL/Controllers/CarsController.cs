using Microsoft.AspNetCore.Mvc;
using Scan.BLL.Dto_s;
using Scan.BLL.Services.CarServices;
using Scan.DAL.Models.Car;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Scan.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController(ICarService carService) : ControllerBase
    {


        //[HttpPost("Add")]
        //public async Task<IActionResult> AddCars(CarDto carDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //    if (string.IsNullOrEmpty(userId))
        //        return Unauthorized(new { error = "Invalid authentication token." });

        //    var createdCar = await carService.AddCarAsync(carDto, userId);

        //    return Ok(createdCar);
        //}

        [HttpGet("{carId:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid carId)
        {
           

           

            var car = await carService.GetCarByIdAsync( carId);
            if (car == null)
                return NotFound();
            return Ok(car);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { error = "Invalid authentication token." });



            var cars = await carService.GetUserCarsAsync(userId);

            return Ok(cars);
        }

    }
}
