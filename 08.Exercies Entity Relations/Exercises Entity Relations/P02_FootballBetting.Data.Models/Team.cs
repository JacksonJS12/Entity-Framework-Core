using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models;
public class Team
{
    [Key] //Set PK of the table -> UNIQUE, NOT NULL
    public int TeamId { get; set; }
    
    [Required] //NOT NULL constrain in SQL
    [MaxLength(vALIDA)]
    public string Name { get; set; }
}
