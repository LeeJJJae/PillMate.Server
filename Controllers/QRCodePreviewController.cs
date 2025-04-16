using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;
using QRCoder;
using System.Text.Json;
using System.Text.Encodings.Web; // 👈 추가 여기!

namespace PillMate.Server.Controllers
{
    [Route("api/qrcode/preview")]
    [ApiController]
    public class QRCodePreviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QRCodePreviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> Preview(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
                return NotFound("환자 정보가 없습니다.");

            var qrData = new
            {
                patient.Hwanja_No,
                patient.Hwanja_Name
            };

            // ✅ 여기 수정
            var jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string jsonText = JsonSerializer.Serialize(qrData, jsonOptions);

            // QR 생성
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(jsonText, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new Base64QRCode(qrCodeData);
            var qrImageBase64 = qrCode.GetGraphic(20);

            // HTML 렌더링
            var html = $@"
    <html>
    <head>
        <meta charset='UTF-8'>
        <title>QR 미리보기</title>
    </head>
    <body style='font-family:sans-serif;text-align:center;padding:30px'>
        <h2>📷 QR 코드 미리보기</h2>
        <img src='data:image/png;base64,{qrImageBase64}' style='width:250px;height:250px;border:1px solid #ccc;'><br/><br/>
        <h3>📦 QR에 담긴 데이터</h3>
        <pre style='background:#f4f4f4;padding:10px;border-radius:5px;border:1px solid #ccc;text-align:left;'>{jsonText}</pre>
    </body>
    </html>";


            return Content(html, "text/html");
        }
    }
}
