using Microsoft.EntityFrameworkCore;
using PillMate.Server.Models;

namespace PillMate.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Pill> Pills { get; set; }
        public DbSet<BukyoungStatus> BukyoungStatuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ 환자 삭제 시 복약 상태도 같이 삭제되도록 설정
            modelBuilder.Entity<BukyoungStatus>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.BukyoungStatuses)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
