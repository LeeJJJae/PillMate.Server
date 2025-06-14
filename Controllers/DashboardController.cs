using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.DTO;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<DashboardDto>> GetDashboardSummary()
        {
            var totalPatients = await _context.Patients.CountAsync();
            var completed = await _context.BukyoungStatuses
                .Where(s => s.Bukyoung_Chk)
                .Select(s => s.PatientId)
                .Distinct()
                .CountAsync();

            var pending = totalPatients - completed;

            return Ok(new DashboardDto
            {
                TotalPatients = totalPatients,
                Completed = completed,
                Pending = pending
            });
        }

        [HttpGet("medications")]
        public async Task<IActionResult> GetPatientMedications()
        {
            var data = await _context.BukyoungStatuses
                .Include(bs => bs.Patient)
                .Select(bs => new
                {
                    PatientName = bs.Hwanja_Name ?? bs.Patient.Hwanja_Name,
                    PillName = _context.TakenMedicines
                        .Include(tm => tm.Pill)  
                        .Where(tm => tm.PatientId == bs.PatientId)
                        .Select(tm => tm.Pill.Yank_Name)
                        .FirstOrDefault(), // 하나만
                    IsTaken = bs.Bukyoung_Chk
                })
                .ToListAsync();

            return Ok(data);
        }
    }
}
