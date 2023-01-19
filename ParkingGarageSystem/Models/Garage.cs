namespace ParkingGarageSystem.Models
{
    public class Garage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalSpots { get; set; }
        public int AvailableSpots { get; set; }
        public string Address { get; set; }
        public string OperatingHours { get; set; }
        public bool Security { get; set; }
        public bool Accessibility { get; set; }
        public string Photos { get; set; }
        public string Description { get; set; }
    }
}
