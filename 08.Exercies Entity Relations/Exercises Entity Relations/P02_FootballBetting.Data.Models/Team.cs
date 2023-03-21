using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;
public class Team
{
    public Team()
    {
        this.HomeGames = new HashSet<Game>();
        this.AwayGames = new HashSet<Game>();
        this.PLayers = new HashSet<PLayer>();
    }
    [Key] //Set PK of the table -> UNIQUE, NOT NULL
    public int TeamId { get; set; }

    [Required] //NOT NULL constrain in SQL
    [MaxLength(ValidationConstants.TeamNameMxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.TeamNameMxLength)]
    public string? LogoUrl { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TeamInitialsMaxLength)]
    public string Initials { get; set; } = null!;
    [ForeignKey(nameof(PrimaryKitColor))]
    public int PrimaryKitColorId { get; set; }
    public virtual Color PrimaryKitColor { get; set; } = null!;
    [ForeignKey(nameof(SecondaryKitColor))]
    public int SecondaryKitColorId { get; set; }
    public virtual Color SecondaryKitColor { get; set; } = null!;
    public decimal Budget { get; set; }

    [ForeignKey(nameof(Team))]
    public int TownId { get; set; }
    public virtual Town Town { get; set; }

    [InverseProperty(nameof(Game.HomeTeam))]
    public virtual ICollection<Game> HomeGames { get; set; }

    [InverseProperty(nameof(Game.AwayTeam))]
    public virtual ICollection<Game> AwayGames { get; set; }

    public virtual ICollection<PLayer> PLayers { get; set; }
}
