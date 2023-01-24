using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class ReservationService : IReservation
    {
        public readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public ReservationService(ParkingSystemDbContext ParkingSystemDbContext)
        {
            _ParkingSystemDbContext = ParkingSystemDbContext;
        }

        public async Task<Reservation> CreateReservation(Reservation reservation)
        {
            _ParkingSystemDbContext.Reservations.Add(reservation);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> GetReservationById(int id)
        {
            return await _ParkingSystemDbContext.Reservations.FindAsync(id);
        }

        public async Task<List<Reservation>> GetReservationsByDateRange(DateTime startDate, DateTime endDate)
        {
            return await _ParkingSystemDbContext.Reservations.Where(r => r.StartTime >= startDate && r.EndTime <= endDate).ToListAsync();
        }
    }
}
