using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Color
{
    public Color()
    {
        this.PrimaryKitTeams = new HashSet<Team>();
        this.SecondaryKitTeams = new HashSet<Team>();
    }

    [Key]
    public int ColorId { get; set; }
    
    [MaxLength(ValidationConstants.ColorNameMaxLength)]
    public string Name { get; set; } = null!;

    [InverseProperty(nameof(Team.PrimaryKitColor))]
    public virtual ICollection<Team> PrimaryKitTeams { get; set; }

    [InverseProperty("SecondaryKitColor")]
    public ICollection<Team> SecondaryKitTeams{ get; set; } 

}
