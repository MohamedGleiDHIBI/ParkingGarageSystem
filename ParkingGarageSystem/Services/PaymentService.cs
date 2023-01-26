using Microsoft.EntityFrameworkCore;
using ParkingGarageSystem.Infrastructure;
using ParkingGarageSystem.Interfaces;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Services
{
    public class PaymentService : IPayment
    {
        private readonly ParkingSystemDbContext _ParkingSystemDbContext;
        public PaymentService(ParkingSystemDbContext parkingSystemDbContext)
        {
            _ParkingSystemDbContext = parkingSystemDbContext;
        }
        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _ParkingSystemDbContext.Payments.ToListAsync();
        }

        public async Task<Payment> GetPayment(int id)
        {
            return await _ParkingSystemDbContext.Payments.FindAsync(id);
        }

        public async Task<Payment> AddPayment(Payment payment)
        {
            _ParkingSystemDbContext.Payments.Add(payment);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return payment;
        }

        public async Task<Payment> UpdatePayment(Payment payment)
        {
            _ParkingSystemDbContext.Payments.Update(payment);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return payment;
        }
        public async Task<bool> DeletePayment(int id)
        {
            var payment = await _ParkingSystemDbContext.Payments.FindAsync(id);
            if (payment == null)
            {
                return false;
            }
            _ParkingSystemDbContext.Payments.Remove(payment);
            await _ParkingSystemDbContext.SaveChangesAsync();
            return true;
        }
    }
}
