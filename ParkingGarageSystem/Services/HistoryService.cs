using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class HistoryService : IHistory
    {
        private readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public HistoryService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }

        public async Task<IEnumerable<History>> GetAllHistories()
        {
            return await _ParkingSystemDbContext.Histories.ToListAsync();
        }

        public async Task<History> GetHistory(int id)
        {
            return await _ParkingSystemDbContext.Histories.FindAsync(id);
        }

        public async Task AddHistory(History history)
        {
            _ParkingSystemDbContext.Histories.Add(history);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task UpdateHistory(History history)
        {
            _ParkingSystemDbContext.Histories.Update(history);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }

        public async Task DeleteHistory(int id)
        {
            var history = await _ParkingSystemDbContext.Histories.FindAsync(id);
            _ParkingSystemDbContext.Histories.Remove(history);
            await _ParkingSystemDbContext.SaveChangesAsync();
        }
    }
}
