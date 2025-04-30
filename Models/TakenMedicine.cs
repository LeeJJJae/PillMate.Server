using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PillMate.Server.Models
{
    public class TakenMedicine
    {
        [Key]
        public int Id { get; set; }

        public int PatientId { get; set; }
        public int PillId { get; set; }

        public string? Dosage { get; set; } // 예: 하루 2번, 식후 복용 등

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        [ForeignKey("PillId")]
        public Pill? Pill { get; set; }
    }
}
