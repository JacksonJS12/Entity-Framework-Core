namespace Boardgames.DataProcessor.ImportDto;

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

public class ImportSellerDto
{

    [JsonProperty("Name")]
    [Required]
    [StringLength(20, MinimumLength = 5)]
    public string Name { get; set; } = null!;

    [JsonProperty("Address")]
    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string Address { get; set; } = null!;

    [JsonProperty("Country")]
    [Required]
    public string Country { get; set; } = null!;

    [JsonProperty("Website")]
    [Required]
    [RegularExpression(@"^[www]{3}\.{1}[A-Za-z\d\-]+\.com{1}$")]
    public string Website { get; set; } = null!;

    [JsonProperty("Boardgames")]
    public int[] Boardgames { get; set; }
}
