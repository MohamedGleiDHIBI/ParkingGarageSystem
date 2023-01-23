using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class UserManagementsService : IUserManagements
    {
        public readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public UserManagementsService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                _ParkingSystemDbContext.Users.Add(user);
                await _ParkingSystemDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _ParkingSystemDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _ParkingSystemDbContext.Users.FindAsync(userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                _ParkingSystemDbContext.Users.Update(user);
                await _ParkingSystemDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteUser(int id)
        {
            var user = await _ParkingSystemDbContext.Users.FindAsync(id);
            if (user == null)
            {
                return false;
            }
            _ParkingSystemDbContext.Users.Remove(user);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return true;
        }
    }
}
