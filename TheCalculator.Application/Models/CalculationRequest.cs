using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TheCalculator.Application.Models
{
    public class CalculationRequest
    {
        [Required]
        [JsonPropertyName("expression")]
        public string Request { get; set; }
    }
}
