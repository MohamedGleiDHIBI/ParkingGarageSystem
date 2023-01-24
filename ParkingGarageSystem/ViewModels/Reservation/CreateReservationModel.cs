namespace ParkingGarageSystem.ViewModels.Reservation
{
    public class CreateReservationModel
    {
        public int LocationId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
