using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Services;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicle _Vehicle;
        public VehicleController(IVehicle vehicle)
        {
            _Vehicle = vehicle;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await _Vehicle.GetVehicle(id);
            if (vehicle == null)
                return NotFound();

            return Ok(vehicle);
        }
    }
}
