using Microsoft.AspNetCore.Mvc;
using zad5.Models;
using zad5.Data;

namespace Zadanie6.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll([FromQuery] DateOnly? date, [FromQuery] string? status, [FromQuery] int? roomId)
        {
            var query = DataStore.Reservations.AsQueryable();

            if (date.HasValue) query = query.Where(r => r.Date == date.Value);
            if (!string.IsNullOrEmpty(status)) query = query.Where(r => r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
            if (roomId.HasValue) query = query.Where(r => r.RoomId == roomId.Value);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Reservation> GetById(int id)
        {
            var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public ActionResult<Reservation> Create([FromBody] Reservation res)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == res.RoomId);
            
            if (room == null) return NotFound();
            if (!room.IsActive) return BadRequest();

            bool hasConflict = DataStore.Reservations.Any(existing => 
                existing.RoomId == res.RoomId && 
                existing.Date == res.Date && 
                res.StartTime < existing.EndTime && res.EndTime > existing.StartTime);

            if (hasConflict) return Conflict();

            res.Id = DataStore.Reservations.Any() ? DataStore.Reservations.Max(r => r.Id) + 1 : 1;
            DataStore.Reservations.Add(res);

            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Reservation updatedRes)
        {
            var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();

            bool hasConflict = DataStore.Reservations.Any(existing => 
                existing.Id != id &&
                existing.RoomId == updatedRes.RoomId && 
                existing.Date == updatedRes.Date && 
                updatedRes.StartTime < existing.EndTime && updatedRes.EndTime > existing.StartTime);

            if (hasConflict) return Conflict();

            res.OrganizerName = updatedRes.OrganizerName;
            res.Topic = updatedRes.Topic;
            res.Date = updatedRes.Date;
            res.StartTime = updatedRes.StartTime;
            res.EndTime = updatedRes.EndTime;
            res.Status = updatedRes.Status;
            res.RoomId = updatedRes.RoomId;

            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = DataStore.Reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();

            DataStore.Reservations.Remove(res);
            return NoContent();
        }
    }
}