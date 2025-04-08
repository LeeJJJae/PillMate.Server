using Microsoft.EntityFrameworkCore;
using PillMate.Server.Data;

var builder = WebApplication.CreateBuilder(args);

// ✅ AppDbContext 존재 확인 로그 (선택)
Console.WriteLine($"✔ AppDbContext 존재 확인: {typeof(AppDbContext).FullName}");

// 컨트롤러 서비스 등록
builder.Services.AddControllers();

// DbContext 등록
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 29))
    ));

// Swagger(OpenAPI) 설정
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger UI 사용
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// ✅ 컨트롤러 매핑 필수
app.MapControllers();

app.Run();
