using System;
using System.ComponentModel.DataAnnotations;

namespace PillMate.Server.Models
{
    public class Pill
    {
        [Key]
        public int Id { get; set; }

        // 약품 이름 (예: 타이레놀)
        [Required]
        public string Yank_Name { get; set; }

        // 약품 고유 번호 (식별 코드)
        public string? Yank_Num { get; set; }

        // 현재 재고 수량
        public int Yank_Cnt { get; set; }

        // 제조사 정보 (예: 한미약품, 대웅제약)
        public string? Manufacturer { get; set; }

        // 약품 종류 (예: 정제, 캡슐, 시럽 등)
        public string? Category { get; set; }

        // 유통기한
        public DateTime? ExpirationDate { get; set; }

        // 약품 설명 또는 비고
        public string? Description { get; set; }

        // 보관 위치 (예: 창고 1층, 진열대 A-2)
        public string? StorageLocation { get; set; }
    }
}
