using System.Text.Json.Serialization;

namespace APIClient
{
    public class ExchangeRateResponseDto
    {
        [JsonPropertyName("table")]
        public string Table { get; set; } = string.Empty;
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("rates")]
        public List<Rate> Rates { get; set; }
    }

    public class Rate
    {
        [JsonPropertyName("no")]
        public string Number { get; set; } = string.Empty;
        [JsonPropertyName("effectiveDate")]
        public string EffectiveDate { get; set; } = string.Empty;
        [JsonPropertyName("mid")]
        public double ExchangeRate { get; set; }
    }
}
