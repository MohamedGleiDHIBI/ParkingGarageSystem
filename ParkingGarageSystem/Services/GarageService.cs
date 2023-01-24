using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class GarageService : IGarage
    {
        private readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public GarageService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }

        public async Task<List<Garage>> GetAllGarages()
        {
            return await _ParkingSystemDbContext.Garages.ToListAsync();
        }
    }
}
