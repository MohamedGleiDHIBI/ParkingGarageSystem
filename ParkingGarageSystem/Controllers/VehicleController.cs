using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
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

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddVehicle([FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _Vehicle.AddVehicle(vehicle);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _Vehicle.UpdateVehicle(id, vehicle);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _Vehicle.DeleteVehicle(id);
            if (result == false)
                return NotFound();

            return NoContent();
        }
    }
}
