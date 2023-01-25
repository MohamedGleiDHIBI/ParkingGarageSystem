using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface ILocation
    {
        Task<IEnumerable<Location>> GetAllLocations();
        Task<Location> GetLocationById(int id);
        Task CreateLocation(Location location);
        Task UpdateLocation(Location location);
        Task DeleteLocation(int id);
    }
}
