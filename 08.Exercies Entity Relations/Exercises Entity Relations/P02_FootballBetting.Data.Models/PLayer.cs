using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.AccessControl;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;
public class PLayer
{
    public PLayer()
    {
        this.PlayerStatistics = new HashSet<PlayerStatistic>();
    }
    [Key]
    public int PlayerId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.PlayerNameMaxLength)]
    public string Name { get; set; } = null!;

    public int SquadNumber { get; set; }
    public bool IsInjured { get; set; }

    [ForeignKey(nameof(Team))]
    public int? TeamId { get; set; } //nullable
    public virtual Team? Team { get; set; }

    [ForeignKey(nameof(Position))]
    public int PositionId { get; set; }
    public virtual Position Position { get; set; } = null!;

    public virtual ICollection<PlayerStatistic> PlayerStatistics { get; set; }
}

