namespace ParkingGarageSystem.ViewModels.History
{
    public class HistoryModelView
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Cost { get; set; }
    }
}
