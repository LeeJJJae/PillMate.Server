using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.Models;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PillsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PillsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/pills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pill>>> GetPills()
        {
            return await _context.Pills.ToListAsync();
        }

        // GET: api/pills/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pill>> GetPill(int id)
        {
            var pill = await _context.Pills.FindAsync(id);
            if (pill == null)
                return NotFound();

            return pill;
        }

        // POST: api/pills
        [HttpPost]
        public async Task<ActionResult<Pill>> CreatePill(Pill pill)
        {
            _context.Pills.Add(pill);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPill), new { id = pill.Id }, pill);
        }

        // PUT: api/pills/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePill(int id, Pill pill)
        {
            if (id != pill.Id)
                return BadRequest();

            _context.Entry(pill).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/pills/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePill(int id)
        {
            var pill = await _context.Pills.FindAsync(id);
            if (pill == null)
                return NotFound();

            _context.Pills.Remove(pill);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
