﻿namespace ParkingGarageSystem.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string Color { get; set; }
    }
}
