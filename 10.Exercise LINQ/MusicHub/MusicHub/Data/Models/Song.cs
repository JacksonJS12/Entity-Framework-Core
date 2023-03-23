namespace MusicHub.Data.Models;

using System.Security.AccessControl;
using MusicHub.Data.Models.Enums;

using System.ComponentModel.DataAnnotations;

public class Song
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.SongNameMaxLength)]
    public string Name { get; set; } = null!;

    //TimeSpan is required by default 
    public TimeSpan Duration { get; set; }
    public DateTime CreatedOn { get; set; } 
    public Genre Genre { get; set; }
    public int? AlbumId { get; set; }
    public int WriterId { get; set; }
    public decimal Price { get; set; }
    
}

