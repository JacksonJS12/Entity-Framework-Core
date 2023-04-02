using System.ComponentModel.DataAnnotations;
using Trucks.Common;

namespace Trucks.DataProcessor.ImportDto;

using System.Xml.Serialization;

[XmlType("Truck")]
public class ImportTruckDto
{
    [XmlElement("RegistrationNumber")]
    [MinLength(ValidationConstants.TruckRegistrationNumberLength)]
    [MaxLength(ValidationConstants.TruckRegistrationNumberLength)]
    [RegularExpression(ValidationConstants.TruckRegistrationNumberRegEx)]
    public string? RegistrationNumber { get; set; }

    [XmlElement("VinNumber")]
    [Required]
    [MinLength(ValidationConstants.TruckVinNumberLength)]
    [MaxLength(ValidationConstants.TruckVinNumberLength)]
    public string VinNumber { get; set; } = null!;

    [XmlElement("TankCapacity")]
    [Range(ValidationConstants.TruckTankCapacityMinValue,
        ValidationConstants.TruckTankCapacityMaxValue)]
    public int TankCapacity { get; set; }

    [XmlElement("CargoCapacity")]
    [Range(ValidationConstants.TruckCargoCapacityMinValue,
        ValidationConstants.TruckCargoCapacityMaxValue)]
    public int CargoCapacity { get; set; }

    [XmlElement("CategoryType")]
    [Range(ValidationConstants.TruckCategoryMinValue,
        ValidationConstants.TruckCategoryMaxValue)]
    public int CategoryType { get; set; }

    [XmlElement("MakeType")]
    [Range(ValidationConstants.TruckMakeTypeMinValue,
        ValidationConstants.TruckMakeTypeMaxValue)]
    public int MakeType { get; set; }
}