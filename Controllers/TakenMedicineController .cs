using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.Models;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TakenMedicineController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TakenMedicineController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TakenMedicine/patient/5
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<TakenMedicine>>> GetByPatient(int patientId)
        {
            return await _context.TakenMedicines
                .AsNoTracking() // ✅ 중복 추적 방지
                .Include(tm => tm.Pill)
                .Where(tm => tm.PatientId == patientId)
                .ToListAsync();
        }

        // POST: api/TakenMedicine
        [HttpPost]
        public async Task<ActionResult<TakenMedicine>> AddTakenMedicine(TakenMedicine tm)
        {
            _context.TakenMedicines.Add(tm);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByPatient), new { patientId = tm.PatientId }, tm);
        }

        // DELETE: api/TakenMedicine/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tm = await _context.TakenMedicines.FindAsync(id);
            if (tm == null)
                return NotFound();

            _context.TakenMedicines.Remove(tm);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
