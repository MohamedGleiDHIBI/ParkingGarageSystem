using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Services;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarage _Garage;
        public GarageController(IGarage Garage)
        {
            _Garage = Garage;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGarages()
        {
            var garages = await _Garage.GetAllGarages();
            return Ok(garages);
        }
    }
}
