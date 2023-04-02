using System.ComponentModel.DataAnnotations;
using Boardgames.Common;

namespace Boardgames.DataProcessor.ImportDto;

using Newtonsoft.Json;

public class ImportSellersDto
{
    [JsonProperty("Name")]
    [Required]
    [MinLength(ValidationConstants.SellerNameMinLength)]
    [MaxLength(ValidationConstants.SellerNameMaxLength)]
    public string Name { get; set; } = null!;

    [JsonProperty("Address")]
    [Required]
    [MinLength(ValidationConstants.SellerAddressMinLength)]
    [MaxLength(ValidationConstants.SellerAddressMaxLength)]
    public string Address { get; set; } = null!;

    [JsonProperty("Country")]
    [Required]
    public string Country { get; set; } = null!;

    [JsonProperty("Website")]
    [Required]
    [RegularExpression(ValidationConstants.SellerWebsiteRegEx)]
    public string Website { get; set; } = null!;

    [JsonProperty("Boardgames")]
    public int[] BoardgamesIds { get; set; } = null!;
}