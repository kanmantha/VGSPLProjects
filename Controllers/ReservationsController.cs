using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RailwayReservation.Web.Data;
using RailwayReservation.Web.Models;

namespace RailwayReservation.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ReservationsController(AppDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Reservations.OrderBy(r => r.TravelDate).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _db.Reservations.FindAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Reservation r)
        {
            _db.Reservations.Add(r);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = r.Id }, r);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Reservation r)
        {
            var existing = await _db.Reservations.FindAsync(id);
            if (existing == null) return NotFound();
            existing.PassengerName = r.PassengerName;
            existing.FromStation = r.FromStation;
            existing.ToStation = r.ToStation;
            existing.TravelDate = r.TravelDate;
            existing.Coach = r.Coach;
            existing.SeatNumber = r.SeatNumber;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Reservations.FindAsync(id);
            if (existing == null) return NotFound();
            _db.Reservations.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
