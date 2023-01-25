using ParkingGarageSystem.Models;
using ParkingGarageSystem.Tools;

namespace ParkingGarageSystem.Interfaces
{
    public interface IGarage
    {
        Task<List<Garage>> GetAllGarages();
        Task<Garage> GetGarageById(int id);
        Task AddGarage(Garage garage);
        Task UpdateGarage(Garage garage);
        Task DeleteGarage(int id);
        Task<List<Garage>> SearchGaragesByLocation(string location, int radius);
        Task<List<Garage>> FilterGarages(bool security, bool accessibility);
        Task<Occupancy> GetOccupancy(int id);
        Task<bool> GetSpotAvailability(int spotId);
    }
}
