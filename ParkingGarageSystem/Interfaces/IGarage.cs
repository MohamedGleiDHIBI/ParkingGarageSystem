using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IGarage
    {
        Task<List<Garage>> GetAllGarages();
    }
}
