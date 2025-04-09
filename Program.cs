using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// 서비스 등록
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29))
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 정적 파일 (선택사항, 없으면 생략 가능)
// app.UseStaticFiles(); <-- Swagger UI 자체는 이거 없이도 잘 뜸

// HTTPS 강제 리디렉션 제거 (CLI 실행 시 필요 없음)
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// ✅ Swagger UI 반드시 여기서 등록해야 Swagger 페이지 열림
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PillMate.Server v1");
    c.RoutePrefix = "swagger"; // http://localhost:5000/swagger
});

app.Run();
