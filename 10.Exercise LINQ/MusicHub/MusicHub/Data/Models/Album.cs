namespace MusicHub.Data.Models;

using System.ComponentModel.DataAnnotations;

public class Album
{
    [Key]
    public int Id { get; set; }

    [MaxLength(ValidationConstants.AlbumNameMaxLength)]
    public string Name { get; set; } = null!;

    public DateTime ReleaseDate { get; set; }
    public int? ProducerId { get; set; }

}

