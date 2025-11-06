using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PillMate.Server.Models
{
    public class PrescriptionItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PrescriptionRecordId { get; set; }

        [ForeignKey("PrescriptionRecordId")]
        public PrescriptionRecord PrescriptionRecord { get; set; }

        [Required]
        public int PillId { get; set; }

        [ForeignKey("PillId")]
        public Pill Pill { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
