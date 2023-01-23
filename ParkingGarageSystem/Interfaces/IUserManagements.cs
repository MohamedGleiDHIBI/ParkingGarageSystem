using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IUserManagements
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUserByEmail(string email);
    }
}
