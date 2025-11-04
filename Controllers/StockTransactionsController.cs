using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using PillMate.Server.Models;
using PillMate.Server.DTO;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockTransactionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StockTransactionsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ 전체 출고 내역 조회 (DTO 변환 적용)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockTransactionDto>>> GetAll()
        {
            var transactions = await _context.StockTransactions
                .Include(s => s.Pill)
                .OrderByDescending(s => s.ReleasedAt)
                .Select(s => new StockTransactionDto
                {
                    Id = s.Id,
                    PillId = s.PillId,
                    PillName = s.Pill != null ? s.Pill.Yank_Name : "(정보 없음)",
                    Quantity = s.Quantity,
                    ReleasedAt = s.ReleasedAt,
                    PharmacistName = s.PharmacistName,
                    Note = s.Note
                })
                .ToListAsync();

            return Ok(transactions);
        }

        // ✅ 단일 출고 내역 조회
        [HttpGet("{id}")]
        public async Task<ActionResult<StockTransactionDto>> GetById(int id)
        {
            var s = await _context.StockTransactions
                .Include(x => x.Pill)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (s == null)
                return NotFound();

            var dto = new StockTransactionDto
            {
                Id = s.Id,
                PillId = s.PillId,
                PillName = s.Pill?.Yank_Name ?? "(정보 없음)",
                Quantity = s.Quantity,
                ReleasedAt = s.ReleasedAt,
                PharmacistName = s.PharmacistName,
                Note = s.Note
            };

            return Ok(dto);
        }

        // ✅ 출고 내역 등록
        [HttpPost]
        public async Task<ActionResult<StockTransaction>> Create(CreateStockTransactionDto dto)
        {
            var pill = await _context.Pills.FindAsync(dto.PillId);
            if (pill == null)
                return BadRequest("존재하지 않는 약품입니다.");

            if (pill.Yank_Cnt < dto.Quantity)
                return BadRequest("재고가 부족합니다.");

            pill.Yank_Cnt -= dto.Quantity;

            var newRecord = new StockTransaction
            {
                PillId = dto.PillId,
                Quantity = dto.Quantity,
                ReleasedAt = DateTime.Now,
                PharmacistName = dto.PharmacistName,
                Note = dto.Note
            };

            _context.StockTransactions.Add(newRecord);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = newRecord.Id }, newRecord);
        }

        // ✅ 출고 내역 수정
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StockTransaction updated)
        {
            if (id != updated.Id)
                return BadRequest();

            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ 출고 내역 삭제
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var record = await _context.StockTransactions.FindAsync(id);
            if (record == null)
                return NotFound();

            _context.StockTransactions.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
