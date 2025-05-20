using Microsoft.AspNetCore.Mvc;
using PillMate.Server.Models;
using PillMate.Server.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace PillMate.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
                return BadRequest("이미 존재하는 사용자 이름입니다.");

            user.PasswordHash = ComputeHash(user.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("회원가입 성공");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var hashedPassword = ComputeHash(loginUser.PasswordHash);
            var user = _context.Users.SingleOrDefault(u => u.Username == loginUser.Username && u.PasswordHash == hashedPassword);
            if (user == null)
                return Unauthorized("사용자 이름 또는 비밀번호가 틀렸습니다.");

            // JWT 토큰 발급 코드 등 추가 가능 (현재는 단순 사용자 정보 반환)
            return Ok(new { user.Id, user.Username, user.Email });
        }

        private string ComputeHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
