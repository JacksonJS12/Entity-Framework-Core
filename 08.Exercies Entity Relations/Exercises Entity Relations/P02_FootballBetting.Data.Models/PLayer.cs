using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;
public class PLayer
{
    [Key]
    public int PlayerId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.PlayerNameMaxLength)]
    public string Name { get; set; }

    public int SquadNumber { get; set; }
    public bool IsInjured { get; set; }
    public int? TeamId { get; set; } //nullable
    public int PositionId { get; set; }
}

