using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PillMate.Server.Models
{
    public class StockTransaction
    {
        [Key]
        public int Id { get; set; }

        // 출고된 약품 ID (필수)
        [Required]
        public int PillId { get; set; }

        // 약품 정보 (탐색 속성) → nullable로 수정 ✅
        [ForeignKey("PillId")]
        public Pill? Pill { get; set; }

        // (선택) 특정 환자에게 출고된 경우
        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }

        // 출고 수량
        [Required]
        public int Quantity { get; set; }

        // 출고 일시
        [Required]
        public DateTime ReleasedAt { get; set; } = DateTime.Now;

        // 담당 약사 이름 (필수)
        [Required]
        [StringLength(50)]
        public string PharmacistName { get; set; } = string.Empty;

        // 비고나 메모
        public string? Note { get; set; }
    }
}
