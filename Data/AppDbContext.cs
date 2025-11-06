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
        public DbSet<TakenMedicine> TakenMedicines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PrescriptionRecord> PrescriptionRecords { get; set; }
        public DbSet<PrescriptionItem> PrescriptionItems { get; set; }

        // ✅ 새로 추가되는 출고 기록 테이블
        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ✅ 환자 삭제 시 복약 상태도 같이 삭제되도록 설정
            modelBuilder.Entity<BukyoungStatus>()
                .HasOne(b => b.Patient)
                .WithMany(p => p.BukyoungStatuses)
                .HasForeignKey(b => b.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ 환자 삭제 시 복용 약품 기록도 같이 삭제되도록 설정
            modelBuilder.Entity<TakenMedicine>()
                .HasOne(tm => tm.Patient)
                .WithMany(p => p.TakenMedicines)
                .HasForeignKey(tm => tm.PatientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ StockTransaction 관계 설정
            modelBuilder.Entity<StockTransaction>()
                .HasOne(s => s.Pill)
                .WithMany()
                .HasForeignKey(s => s.PillId)
                .OnDelete(DeleteBehavior.Cascade);  // 약품 삭제 시 출고 기록도 삭제됨

            modelBuilder.Entity<StockTransaction>()
                .HasOne(s => s.Patient)
                .WithMany()
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.SetNull);  // 환자 삭제 시 출고 기록은 남기되, PatientId를 null로 변경

            modelBuilder.Entity<PrescriptionItem>()
                    .HasOne(pi => pi.PrescriptionRecord)
                    .WithMany(r => r.Items)
                    .HasForeignKey(pi => pi.PrescriptionRecordId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
