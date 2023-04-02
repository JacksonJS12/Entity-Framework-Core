namespace Boardgames.DataProcessor.ImportDto;

using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

[XmlType("Creator")]
public class ImportCreatorDto
{
    [XmlElement("FirstName")]
    [Required]
    [StringLength(7, MinimumLength = 2)]
    public string FirstName { get; set; } = null!;


    [XmlElement("LastName")]
    [Required]
    [StringLength(7, MinimumLength = 2)]
    public string LastName { get; set; } = null!;


    [XmlArray("Boardgames")]
    public ImportCreatorBoardgameDto[] Boardgames { get; set; }
}
