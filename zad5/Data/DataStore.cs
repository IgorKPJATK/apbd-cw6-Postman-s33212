namespace zad5.Data;

using zad5.Models;
using System.Collections.Generic;

public class DataStore
{
    public static List<Rooms> Rooms = new List<Rooms>
    {
        new Rooms { Id = 1, Name = "Lab 101", BuildingCode = "A", Floor = 1, Capacity = 20, HasProjector = true, IsActive = true },
        new Rooms { Id = 2, Name = "Aula", BuildingCode = "A", Floor = 0, Capacity = 100, HasProjector = true, IsActive = true },
        new Rooms { Id = 3, Name = "Salka 202", BuildingCode = "B", Floor = 2, Capacity = 10, HasProjector = false, IsActive = true },
        new Rooms { Id = 4, Name = "Kanciapa", BuildingCode = "C", Floor = 1, Capacity = 5, HasProjector = false, IsActive = false }
    };

    public static List<Reservation> Reservations = new List<Reservation>
    {
        new Reservation { Id = 1, RoomId = 1, OrganizerName = "Jan Kowalski", Topic = "C# Intro", Date = new DateOnly(2026, 5, 10), StartTime = new TimeSpan(8, 0, 0), EndTime = new TimeSpan(10, 0, 0), Status = "confirmed" }
    };
}
