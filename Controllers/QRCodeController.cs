using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using QRCoder;
using Newtonsoft.Json;
using System.Drawing; // Bitmap 필요
using System.IO;       // File IO
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
            // 1. 환자 조회
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
                return NotFound("해당 환자를 찾을 수 없습니다.");

            // 2. 복약 상태에 연결된 약 이름 리스트 수집 (예: 복용 체크된 약)
            var pillNames = await _context.BukyoungStatuses
                .Where(b => b.PatientId == patientId && b.Bukyoung_Chk)
                .Select(b => b.Hwanja_Name) // 실제 약 이름으로 수정 가능
                .ToListAsync();

            // 3. QR에 담을 데이터 구성
            var qrData = new
            {
                patient.Hwanja_No,
                patient.Hwanja_Name,
                Pills = pillNames
            };

            // 4. JSON 직렬화 (한글 깨짐 방지)
            string qrText = JsonConvert.SerializeObject(qrData, Formatting.None);

            // 5. QR 생성
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(20);

            // 6. 서버에 QR 이미지 저장
            string folderPath = @"C:\PillMate.Server\qrimage";
            Directory.CreateDirectory(folderPath); // 폴더 없으면 생성
            string savePath = Path.Combine(folderPath, $"qrcode_{patient.Hwanja_No}.png");
            System.IO.File.WriteAllBytes(savePath, qrCodeImage);

            // 7. 클라이언트로 이미지 반환
            return File(qrCodeImage, "image/png");
        }
    }
}
