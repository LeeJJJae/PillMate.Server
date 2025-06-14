using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PillMate.Server.Data;
using QRCoder;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QRCodeController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QRCodeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GenerateQRCode(int patientId)
        {
            try
            {
                var patient = await _context.Patients.FindAsync(patientId);
                if (patient == null)
                    return NotFound("해당 환자를 찾을 수 없습니다.");

                var takenMedicines = await _context.TakenMedicines
                    .Include(t => t.Pill)
                    .Where(t => t.PatientId == patientId)
                    .Select(t => new
                    {
                        PId = t.PillId,
                        Dosage = t.Dosage
                    })
                    .ToListAsync();

                var qrData = new
                {
                    patient.Hwanja_No,
                    patient.Hwanja_Name,
                    TakenMedicines = takenMedicines
                };

                string qrText = JsonConvert.SerializeObject(qrData, Formatting.None);

                using var qrGenerator = new QRCodeGenerator();
                using var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
                var qrCode = new BitmapByteQRCode(qrCodeData);
                byte[] qrImage = qrCode.GetGraphic(20);

                // 이미지 파일 저장 없이 직접 반환
                return File(qrImage, "image/png");
            }
            catch (Exception ex)
            {
                // 로깅
                /* string logPath = @"C:\PillMate.Server\logs";
                Directory.CreateDirectory(logPath);
                string logFile = Path.Combine(logPath, "qr_errors.log");
                string logMessage = $"[{DateTime.Now}] QR 생성 실패 (환자 ID: {patientId})\n{ex}\n\n";
                await System.IO.File.AppendAllTextAsync(logFile, logMessage); */

                return StatusCode(500, $"QR 코드 생성 실패: {ex.Message}");
            }
        }
    }
}
