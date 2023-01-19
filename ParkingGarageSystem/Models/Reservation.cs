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
        public decimal TotalCost { get; set; }
    }
}
