using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarageSystem.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime CancellationDate { get; set; }
        public string CancellationReason { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalCost { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
