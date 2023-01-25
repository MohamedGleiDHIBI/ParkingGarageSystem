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

        public async Task<Vehicle> GetVehicle(int id)
        {
            return await _ParkingSystemDbContext.Vehicles.FindAsync(id);
        }

        public async Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle)
        {
            var existingVehicle = await _ParkingSystemDbContext.Vehicles.FindAsync(id);
            if (existingVehicle == null)
                return null;

            existingVehicle.Make = vehicle.Make;
            existingVehicle.Model = vehicle.Model;
            existingVehicle.LicensePlate = vehicle.LicensePlate;
            existingVehicle.Type = vehicle.Type;
            existingVehicle.Notes = vehicle.Notes;
            existingVehicle.Color = vehicle.Color;

            _ParkingSystemDbContext.Vehicles.Update(existingVehicle);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return existingVehicle;
        }
    }
}
