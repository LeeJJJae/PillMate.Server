using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PillMate.Server.Models
{
    public class Patient
{
    [Key]
    public int Id { get; set; }

    public string? Hwanja_Name { get; set; }
    public string? Hwanja_Gender { get; set; }
    public int? Hwanja_Age { get; set; }
    public string? Hwanja_No { get; set; }
    public string? Hwanja_Room { get; set; }
    public string? Hwanja_PhoneNumber { get; set; }

    public string? Bohoja_Name { get; set; }
    public string? Bohoja_PhoneNumber { get; set; }

    public List<BukyoungStatus> BukyoungStatuses { get; set; } = new();
    public List<TakenMedicine> TakenMedicines { get; set; } = new();

}

}
