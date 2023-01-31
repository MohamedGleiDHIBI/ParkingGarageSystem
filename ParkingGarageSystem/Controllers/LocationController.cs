using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.ViewModels.LocationView;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocation _Location;
        private readonly IMapper _Mapper;
        public LocationController(ILocation location, IMapper mapper)
        {
            _Location = location;
            _Mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Location>>> GetLocations()
        {
            var locations = await _Location.GetAllLocations();
            return Ok(locations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
            var location = await _Location.GetLocationById(id);
            if (location == null)
            {
                return NotFound();
            }
            return Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<Location>> AddLocation(LocationModelView locationModelView)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var _location = _Mapper.Map<Location>(locationModelView);

            await _Location.CreateLocation(_location);
            return CreatedAtAction("GetLocation", new { id = _location.Id }, _location);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocation(int id, LocationModelView locationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var locationbyId = await _Location.GetLocationById(id);

            if (locationbyId == null)
                return BadRequest(new { message = "Invalid location ID. The entered ID does not match any existing location. Please double check and try again" });

            var location = _Mapper.Map<Location>(locationModel);
            location.Id = id;
            await _Location.UpdateLocation(location);
            return Ok(location);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Location>> DeleteLocation(int id)
        {
            await _Location.DeleteLocation(id);
            return NoContent();
        }
    }
}
