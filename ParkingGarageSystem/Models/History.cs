using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingGarageSystem.Models
{
    public class History
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }
        public virtual User User { get; set; }
        public virtual Location Location { get; set; }
    }
}
