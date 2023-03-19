using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;

public class Town
{
    [Key]
    public int TownId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.TownNameMaxLength)]
    public string Name { get; set; }

    public int CountryId { get; set; }
}

