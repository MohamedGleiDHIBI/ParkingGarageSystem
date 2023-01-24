﻿namespace ParkingGarageSystem.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public DateTime lastLoginTime { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public bool IsValidated { get; set; }
    }
}
