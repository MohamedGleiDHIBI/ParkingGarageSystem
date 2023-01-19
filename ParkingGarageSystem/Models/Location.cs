namespace ParkingGarageSystem.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public int GarageId { get; set; }
        public virtual Garage Garage { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
