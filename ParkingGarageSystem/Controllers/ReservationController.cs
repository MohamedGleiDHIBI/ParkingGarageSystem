using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Services;
using ParkingGarageSystem.ViewModels.Reservation;

namespace ParkingGarageSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservation _Reservation;
        public ReservationController(IReservation Reservation)
        {
            _Reservation = Reservation;
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // Map CreateReservationModel to Reservation
            var reservation = new Reservation
            {
                LocationId = model.LocationId,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                ReservationDate = DateTime.Now,
                UserId = int.Parse(User.Identity.Name)
            };

            var newReservation = await _Reservation.CreateReservation(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = newReservation.Id }, newReservation);
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservationById(int id)
        {
            var reservation = _Reservation.GetReservationById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpGet("date-range")]
        public ActionResult<List<Reservation>> GetReservationsByDateRange(DateTime startDate, DateTime endDate)
        {
            var reservations = _Reservation.GetReservationsByDateRange(startDate, endDate);
            return Ok(reservations);
        }
    }
}
