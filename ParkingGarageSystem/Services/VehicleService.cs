using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class VehicleService : IVehicle
    {
        private readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public VehicleService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }

        public async Task AddVehicle(Vehicle vehicle)
        {
            _ParkingSystemDbContext.Vehicles.Add(vehicle);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteVehicle(int id)
        {
            var vehicle = await _ParkingSystemDbContext.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return false;
            }
            _ParkingSystemDbContext.Vehicles.Remove(vehicle);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await _ParkingSystemDbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle)
        {
            var existingVehicle = _ParkingSystemDbContext.Vehicles.FirstOrDefault(v=>v.Id ==vehicle.Id);
            if (existingVehicle != null)
            {
                _ParkingSystemDbContext.Vehicles.Attach(existingVehicle);
                existingVehicle.Make = vehicle.Make;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.LicensePlate = vehicle.LicensePlate;
                existingVehicle.Type = vehicle.Type;
                existingVehicle.Notes = vehicle.Notes;
                existingVehicle.Color = vehicle.Color;
                await _ParkingSystemDbContext.SaveChangesAsync();
            }
            return existingVehicle;
        }
    }
}
