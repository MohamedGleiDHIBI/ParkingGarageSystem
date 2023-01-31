namespace ParkingGarageSystem.ViewModels.Garage
{
    public class AddGarageViewModel
    {
        public string Name { get; set; }
        public int TotalSpots { get; set; }
        public string Address { get; set; }
        public string OperatingHours { get; set; }
        public bool Security { get; set; }
        public bool Accessibility { get; set; }
        public string Description { get; set; }
        public string Photos { get; set; }
    }
}
