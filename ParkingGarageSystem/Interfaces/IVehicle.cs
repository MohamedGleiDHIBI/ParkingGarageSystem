using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IVehicle
    {
        Task<Vehicle> GetVehicle(int id);
    }
}
