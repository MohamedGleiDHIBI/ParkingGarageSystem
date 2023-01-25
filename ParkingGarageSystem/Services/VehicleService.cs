﻿using ParkingGarageSystem.Infrastructure;
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
    }
}
