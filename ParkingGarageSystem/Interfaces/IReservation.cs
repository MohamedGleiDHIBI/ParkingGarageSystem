﻿using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Interfaces
{
    public interface IReservation
    {
        Task<Reservation> CreateReservation(Reservation reservation);
        Task<Reservation> GetReservationById(int id);
        Task<List<Reservation>> GetReservationsByDateRange(DateTime startDate, DateTime endDate);
    }
}
