using System.ComponentModel.DataAnnotations;

namespace PillMate.Server.Models
{
    public class Pill
{
    [Key]
    public int Id { get; set; }

    public string? Yank_Name { get; set; }
    public int Yank_Cnt { get; set; }
    public string? Yank_Num { get; set; }
}

}
