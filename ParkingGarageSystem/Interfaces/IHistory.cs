using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IHistory
    {
        Task<IEnumerable<History>> GetAllHistories();
        Task<History> GetHistory(int id);
        Task AddHistory(History history);
        Task UpdateHistory(History history);
        Task DeleteHistory(int id);
    }
}
