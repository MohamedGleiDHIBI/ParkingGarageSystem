using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class LocationService : ILocation
    {
        private readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public LocationService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }

        public async Task CreateLocation(Location location)
        {
            _ParkingSystemDbContext.Locations.Add(location);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task DeleteLocation(int id)
        {
            var history = await _ParkingSystemDbContext.Locations.FindAsync(id);
            _ParkingSystemDbContext.Locations.Remove(history);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Location>> GetAllLocations()
        {
            return await _ParkingSystemDbContext.Locations.ToListAsync();
        }

        public async Task<Location> GetLocationById(int id)
        {
            return await _ParkingSystemDbContext.Locations.FindAsync(id);
        }

        public async Task UpdateLocation(Location location)
        {
            var locationUpdate = _ParkingSystemDbContext.Locations.FirstOrDefault(g => g.Id == location.Id);
            if (locationUpdate != null)
            {
                _ParkingSystemDbContext.Locations.Attach(locationUpdate);
                locationUpdate.Name = location.Name;
                locationUpdate.IsAvailable = location.IsAvailable;
                locationUpdate.GarageId = location.GarageId;
                await _ParkingSystemDbContext.SaveChangesAsync();
            }
        }
    }
}
