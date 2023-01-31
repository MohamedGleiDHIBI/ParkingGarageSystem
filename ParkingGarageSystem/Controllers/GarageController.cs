using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Microsoft.Win32;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;
using ParkingGarageSystem.ViewModels.Garage;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : ControllerBase
    {
        private readonly IGarage _Garage;
        private readonly IMapper _Mapper;
        public GarageController(IGarage Garage, IMapper mapper)
        {
            _Garage = Garage;
            _Mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllGarages()
        {
            var garages = await _Garage.GetAllGarages();
            return Ok(garages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGarageById(int id)
        {
            var garage = await _Garage.GetGarageById(id);
            if (garage == null)
            {
                return NotFound();
            }
            return Ok(garage);
        }

        [HttpPost]
        public async Task<IActionResult> AddGarage([FromBody] AddGarageViewModel garageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var garage = _Mapper.Map<Garage>(garageViewModel);
            await _Garage.AddGarage(garage);
            return CreatedAtAction(nameof(GetGarageById), new { id = garage.Id }, garage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGarage(int id, [FromBody] AddGarageViewModel garageViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var garageById= await _Garage.GetGarageById(id);

            if(garageById == null)
                return BadRequest(new { message = "Invalid garage ID. The entered ID does not match any existing garage. Please double check and try again" });

            var garage = _Mapper.Map<Garage>(garageViewModel);
            garage.Id = id;
            await _Garage.UpdateGarage(garage);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGarage(int id)
        {
            await _Garage.DeleteGarage(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchGaragesByLocation([FromQuery] string location, [FromQuery] int radius)
        {
            var garages = await _Garage.SearchGaragesByLocation(location, radius);
            return Ok(garages);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterGarages([FromQuery] bool security, [FromQuery] bool accessibility)
        {
            var garages = await _Garage.FilterGarages(security, accessibility);
            return Ok(garages);
        }

        [HttpGet("{id}/occupancy")]
        public async Task<IActionResult> GetOccupancy(int id)
        {
            var occupancy = await _Garage.GetOccupancy(id);
            return Ok(occupancy);
        }

        [HttpGet("spot/{id}/availability")]
        public async Task<IActionResult> GetSpotAvailability(int spotId)
        {
            var availability = await _Garage.GetSpotAvailability(spotId);
            return Ok(availability);
        }
    }
}
