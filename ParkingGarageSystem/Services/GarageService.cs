using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;
using ParkingGarageSystem.Tools;

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
        public async Task<Garage> GetGarageById(int id)
        {
            return await _ParkingSystemDbContext.Garages.FindAsync(id);
        }
        public async Task AddGarage(Garage garage)
        {
            _ParkingSystemDbContext.Garages.Add(garage);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }
        public async Task UpdateGarage(Garage ToUpdatedGarage)
        {
            var garage = _ParkingSystemDbContext.Garages.FirstOrDefault(g => g.Id == ToUpdatedGarage.Id);
            if (garage != null)
            {
                _ParkingSystemDbContext.Garages.Attach(garage);
                garage.Name = ToUpdatedGarage.Name;
                garage.TotalSpots = ToUpdatedGarage.TotalSpots;
                garage.Address = ToUpdatedGarage.Address;
                garage.OperatingHours = ToUpdatedGarage.OperatingHours;
                garage.Security = ToUpdatedGarage.Security;
                garage.Accessibility = ToUpdatedGarage.Accessibility;
                garage.Description = ToUpdatedGarage.Description;
                garage.Photos = ToUpdatedGarage.Photos;
                await _ParkingSystemDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteGarage(int id)
        {
            var garage = await _ParkingSystemDbContext.Garages.FindAsync(id);
            _ParkingSystemDbContext.Garages.Remove(garage);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task<List<Garage>> SearchGaragesByLocation(string location, int radius)
        {
            return await _ParkingSystemDbContext.Garages
                    .Where(g =>
                        (g.Address.Contains(location) || g.Name.Contains(location)) &&
                        (g.Locations.Any(l => l.IsAvailable))
                        )
                    .ToListAsync();
        }

        public async Task<List<Garage>> FilterGarages(bool security, bool accessibility)
        {
            return await _ParkingSystemDbContext.Garages
                        .Where(g => g.Security == security && g.Accessibility == accessibility)
                        .ToListAsync();
        }

        public async Task<Occupancy> GetOccupancy(int id)
        {
            var garage = await _ParkingSystemDbContext.Garages
                        .Include(g => g.Locations)
                        .FirstOrDefaultAsync(g => g.Id == id);

                            if (garage == null)
                                throw new Exception("Garage not found");
                            var totalSpots = garage.TotalSpots;
                            var occupiedSpots = garage.Locations.Count(l => !l.IsAvailable);

                            return new Occupancy { TotalSpots = totalSpots, OccupiedSpots = occupiedSpots };
        }

        public async Task<bool> GetSpotAvailability(int spotId)
        {
            var location = await _ParkingSystemDbContext.Locations.FindAsync(spotId);
            if (location == null)
                throw new Exception("Location not found.");
            return location.IsAvailable;
        }
    }
}
