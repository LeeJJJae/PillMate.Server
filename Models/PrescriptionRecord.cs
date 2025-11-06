using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PillMate.Server.Models
{
    public class PrescriptionRecord
    {
        [Key]
        public int Id { get; set; }

        // 환자 연결
        [Required]
        public int PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }

        // 기록 정보
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string PharmacistName { get; set; }

        public string Note { get; set; }

        // 여러 약품 정보
        public ICollection<PrescriptionItem> Items { get; set; } = new List<PrescriptionItem>();
    }
}
