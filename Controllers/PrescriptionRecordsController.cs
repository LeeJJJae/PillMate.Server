using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.Models;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionRecordsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PrescriptionRecordsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 환자별 복약 이력 조회
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetByPatient(int patientId)
        {
            var records = await _context.PrescriptionRecords
                .Include(r => r.Items)
                .ThenInclude(i => i.Pill)
                .Where(r => r.PatientId == patientId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();

            return Ok(records.Select(r => new
            {
                r.Id,
                r.CreatedAt,
                r.PharmacistName,
                r.Note,
                Items = r.Items.Select(i => new
                {
                    i.Pill.Yank_Name,
                    i.Quantity
                })
            }));
        }

        // ✅ 새 복약 이력 등록
        [HttpPost]
        public async Task<ActionResult> Create(PrescriptionRecord record)
        {
            _context.PrescriptionRecords.Add(record);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByPatient), new { patientId = record.PatientId }, record);
        }

        // ✅ 이력 재출고
        [HttpPost("{recordId}/reorder")]
        public async Task<ActionResult> Reorder(int recordId)
        {
            var record = await _context.PrescriptionRecords
                .Include(r => r.Items)
                .ThenInclude(i => i.Pill)
                .Include(r => r.Patient)
                .FirstOrDefaultAsync(r => r.Id == recordId);

            if (record == null)
                return NotFound("기록을 찾을 수 없습니다.");

            foreach (var item in record.Items)
            {
                if (item.Pill.Yank_Cnt < item.Quantity)
                    return BadRequest($"{item.Pill.Yank_Name} 재고가 부족합니다.");

                item.Pill.Yank_Cnt -= item.Quantity;

                _context.StockTransactions.Add(new StockTransaction
                {
                    PillId = item.PillId,
                    PatientId = record.PatientId,
                    Quantity = item.Quantity,
                    ReleasedAt = DateTime.Now,
                    PharmacistName = record.PharmacistName,
                    Note = "이전 이력 재출고"
                });
            }

            await _context.SaveChangesAsync();
            return Ok("✅ 재출고 완료");
        }
    }
}
