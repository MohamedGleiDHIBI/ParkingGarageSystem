using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IVehicle
    {
        Task<Vehicle> GetVehicle(int id);
        Task AddVehicle(Vehicle vehicle);
        Task<Vehicle> UpdateVehicle(int id, Vehicle vehicle);
        Task<bool> DeleteVehicle(int id);
    }
}
