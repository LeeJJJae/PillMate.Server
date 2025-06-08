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

            //Console.WriteLine($"[회원가입] 저장된 해시: {user.PasswordHash}");

            return Ok("회원가입 성공");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            var inputHashed = ComputeHash(loginUser.PasswordHash);

            //Console.WriteLine($"[로그인 요청] 사용자명: {loginUser.Username}");
            //Console.WriteLine($"[로그인 요청] 이메일: {loginUser.Email}");
            //Console.WriteLine($"[로그인 요청] 입력된 평문 비밀번호: {loginUser.PasswordHash}");
            //Console.WriteLine($"[로그인 요청] 해시된 입력: {inputHashed}");

            var user = _context.Users
                .SingleOrDefault(u =>
                    u.Username == loginUser.Username &&
                    u.Email == loginUser.Email &&
                    u.PasswordHash == inputHashed);

            if (user == null)
            {
                Console.WriteLine("[로그인 실패] 사용자 없음 또는 정보 불일치");
                return Unauthorized("아이디, 이메일, 비밀번호를 확인해주세요.");
            }

            Console.WriteLine("[로그인 성공]");
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
