using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;
using ParkingGarageSystem.ViewModels.Vehicle;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicle _Vehicle;
        private readonly IMapper _Mapper;
        public VehicleController(IVehicle vehicle,IMapper mapper)
        {
            _Vehicle = vehicle;
            _Mapper = mapper;
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
        public async Task<IActionResult> AddVehicle([FromBody] VehicleViewModel vehicleView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = _Mapper.Map<Vehicle>(vehicleView);
            await _Vehicle.AddVehicle(vehicle);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleViewModel vehicleView)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehiclebyId = await _Vehicle.GetVehicle(id);
            if(vehiclebyId == null)
                return BadRequest(new { message = "Invalid vehicle ID. The entered ID does not match any existing vehicle. Please double check and try again" });
            var vehicle = _Mapper.Map<Vehicle>(vehicleView);
            vehicle.Id = id;
            var result = await _Vehicle.UpdateVehicle(id, vehicle);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _Vehicle.DeleteVehicle(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
