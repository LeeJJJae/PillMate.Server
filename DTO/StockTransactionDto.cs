using System;

namespace PillMate.Server.DTO
{
    public class StockTransactionDto
    {
        public int Id { get; set; }             // 거래 ID
        public int PillId { get; set; }         // 약품 ID
        public string PillName { get; set; }    // 약품 이름
        public int Quantity { get; set; }       // 출고 수량
        public DateTime ReleasedAt { get; set; }// 출고 일시
        public string PharmacistName { get; set; } // 담당 약사
        public string Note { get; set; }        // 비고
    }

    public class CreateStockTransactionDto
    {
        public int PillId { get; set; }         // 약품 ID
        public int Quantity { get; set; }       // 출고 수량
        public string PharmacistName { get; set; } // 담당 약사 이름
        public string? Note { get; set; }       // 비고 (선택)
        public int? PatientId { get; set; }
    }
}
