using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IPayment
    {
        Task<IEnumerable<Payment>> GetPayments();
        Task<Payment> GetPayment(int id);
        Task<Payment> AddPayment(Payment payment);
        Task<Payment> UpdatePayment(Payment payment);
        Task<bool> DeletePayment(int id);
    }
}
