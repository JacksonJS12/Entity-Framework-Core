using System.ComponentModel.DataAnnotations.Schema;

namespace P02_FootballBetting.Data.Models;
public class PlayerStatistic
{
    [ForeignKey(nameof(Game))]
    public int GameId { get; set; }
    public virtual Game Game { get; set; } = null!;

    [ForeignKey(nameof(PLayer))]
    public int PlayerId { get; set; }
    public virtual PLayer PLayer { get; set; } = null!;
    public byte ScoredGoals { get; set; }
    public byte Assists { get; set; }
    public byte MinutesPlayed { get; set; }
}

