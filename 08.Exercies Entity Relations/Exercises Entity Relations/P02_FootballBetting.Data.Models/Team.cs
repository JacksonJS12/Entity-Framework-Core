using System.ComponentModel.DataAnnotations;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;
public class Team
{
    [Key] //Set PK of the table -> UNIQUE, NOT NULL
    public int TeamId { get; set; }

    [Required] //NOT NULL constrain in SQL
    [MaxLength(ValidationConstants.TeamNameMxLength)]
    public string Name { get; set; } = null!;

    [MaxLength(ValidationConstants.TeamNameMxLength)]
    public string LogoUrl { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TeamInitialsMaxLength)]
    public int Initials { get; set; }

    public decimal Budget { get; set; }
}
