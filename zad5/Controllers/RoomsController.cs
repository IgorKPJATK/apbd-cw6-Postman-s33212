using Microsoft.AspNetCore.Mvc;
using zad5.Models;
using zad5.Data;

namespace zad5.Controllers
{
   [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Rooms>> GetAll([FromQuery] int? minCapacity, [FromQuery] bool? hasProjector, [FromQuery] bool? activeOnly)
        {
            var query = DataStore.Rooms.AsQueryable();

            if (minCapacity.HasValue) query = query.Where(r => r.Capacity >= minCapacity.Value);
            if (hasProjector.HasValue) query = query.Where(r => r.HasProjector == hasProjector.Value);
            if (activeOnly == true) query = query.Where(r => r.IsActive);

            return Ok(query.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Rooms> GetById(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpGet("building/{buildingCode}")]
        public ActionResult<IEnumerable<Rooms>> GetByBuilding(string buildingCode)
        {
            var rooms = DataStore.Rooms.Where(r => r.BuildingCode.Equals(buildingCode, StringComparison.OrdinalIgnoreCase));
            return Ok(rooms.ToList());
        }

        [HttpPost]
        public ActionResult<Rooms> Create([FromBody] Rooms room)
        {
            room.Id = DataStore.Rooms.Any() ? DataStore.Rooms.Max(r => r.Id) + 1 : 1;
            DataStore.Rooms.Add(room);
            return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Rooms updatedRoom)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            room.Name = updatedRoom.Name;
            room.BuildingCode = updatedRoom.BuildingCode;
            room.Floor = updatedRoom.Floor;
            room.Capacity = updatedRoom.Capacity;
            room.HasProjector = updatedRoom.HasProjector;
            room.IsActive = updatedRoom.IsActive;

            return Ok(room);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var room = DataStore.Rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return NotFound();

            if (DataStore.Reservations.Any(res => res.RoomId == id))
            {
                return Conflict();
            }

            DataStore.Rooms.Remove(room);
            return NoContent();
        }
    }
}

