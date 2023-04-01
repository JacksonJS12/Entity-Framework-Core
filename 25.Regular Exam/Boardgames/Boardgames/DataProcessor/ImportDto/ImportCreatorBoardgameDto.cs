namespace Boardgames.DataProcessor.ImportDto;

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Boardgames.Data.Models.Enums;

[XmlType("Boardgame")]
public class ImportCreatorBoardgameDto
{
    [XmlElement("Name")]
    [Required]
    [StringLength(20, MinimumLength = 10)]
    public string Name { get; set; } = null!;

    [XmlElement("Rating")]
    [Required]
    [Range(1, 10)]
    public double Rating { get; set; }

    [XmlElement("YearPublished")]
    [Required]
    [Range(2018, 2023)]
    public int YearPublished { get; set; }

    [XmlElement("CategoryType")]
    [Required]
    public string CategoryType { get; set; }

    [XmlElement("Mechanics")]
    [Required]
    public string Mechanics { get; set; } = null!;
}
