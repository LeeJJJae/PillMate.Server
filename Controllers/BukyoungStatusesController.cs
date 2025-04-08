using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.Models;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BukyoungStatusesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BukyoungStatusesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/bukyoungstatuses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BukyoungStatus>>> GetAll()
        {
            return await _context.BukyoungStatuses
                .Include(b => b.Patient) // 환자 정보 포함
                .ToListAsync();
        }

        // GET: api/bukyoungstatuses/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BukyoungStatus>> GetById(int id)
        {
            var record = await _context.BukyoungStatuses
                .Include(b => b.Patient)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (record == null)
                return NotFound();

            return record;
        }

        // POST: api/bukyoungstatuses
        [HttpPost]
        public async Task<ActionResult<BukyoungStatus>> Create(BukyoungStatus status)
        {
            _context.BukyoungStatuses.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = status.Id }, status);
        }

        // PUT: api/bukyoungstatuses/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BukyoungStatus status)
        {
            if (id != status.Id)
                return BadRequest();

            _context.Entry(status).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/bukyoungstatuses/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.BukyoungStatuses.FindAsync(id);
            if (record == null)
                return NotFound();

            _context.BukyoungStatuses.Remove(record);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
