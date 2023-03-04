using System.Text.Json.Serialization;

namespace TheCalculator.Application.Models
{
    public class CalculationResponse
    {
        [JsonPropertyName("result")]
        public decimal Result { get; set; }
    }
}
