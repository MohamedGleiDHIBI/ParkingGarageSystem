using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarageSystem.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
        public string TransactionId { get; set; }
        public virtual Reservation Reservation { get; set; }
    }
}
