using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PillMate.Server.Models
{
    public class BukyoungStatus
    {
        [Key]
        public int Id { get; set; }

        public string? Hwanja_No { get; set; }
        public string? Hwanja_Name { get; set; }

        public bool Bukyoung_Chk { get; set; }

        public int? PatientId { get; set; }

        [ForeignKey("PatientId")]
        public Patient? Patient { get; set; }
    }
}
