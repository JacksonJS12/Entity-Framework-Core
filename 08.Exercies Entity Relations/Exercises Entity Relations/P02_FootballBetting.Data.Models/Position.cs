using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using P02_FootballBetting.Data.Common;

namespace P02_FootballBetting.Data.Models;

public class Position
{
    public Position()
    {
        this.PLayers = new HashSet<PLayer>();
    }
    [Key]
    public int PositionId { get; set; }

    [Required]
    [MaxLength(ValidationConstants.PositionNameMaxLength)]
    public string Name { get; set; } = null!;

    public virtual ICollection<PLayer> PLayers { get; set; }
}

