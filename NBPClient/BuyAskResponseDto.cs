using System.Text.Json.Serialization;

namespace NBPClient
{
    public class BuyAskRateResponseDto
    {
        [JsonPropertyName("table")]
        public string Table { get; set; } = string.Empty;
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        [JsonPropertyName("rates")]
        public List<BuyAskRate> Rates { get; set; }
    }
    public class BuyAskRate
    {
        [JsonPropertyName("no")]
        public string Number { get; set; } = string.Empty;
        [JsonPropertyName("effectiveDate")]
        public string EffectiveDate { get; set; } = string.Empty;
        [JsonPropertyName("bid")]
        public double BidRate { get; set; }
        [JsonPropertyName("ask")]
        public double AskRate { get; set; }

    }
}
