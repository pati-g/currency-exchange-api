using Shared;
using System.Text.Json;

namespace NBPAPIClient
{
    public class NBPClient
    {
        private static readonly HttpClient _client = new();
        private const string _base = "http://api.nbp.pl/api/exchangerates/rates";

        // Date shouldn't be earlier than 2.01.2022
        // Single request shouldn't have span longer than 93 days

        public async Task<double> GetAverageExchangeRateAsync(string date, string currencyCode)
        {
            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/{date}");
            var parsed = await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
            return parsed.Rates[0].ExchangeRate;
        }

        public async Task<MinMaxRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations)
        {
            ValidateQuotationsNumber(lastQuotations);
            ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/last/{lastQuotations}");
            var parsed = await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
            var rates = parsed.Rates.Select(r => r.ExchangeRate).ToList();
            return new MinMaxRateResponseDto { MinimalRate = rates.Min(), MaximumRate = rates.Max() };
        }

        public async Task<double> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations)
        {
            ValidateQuotationsNumber(lastQuotations);
            ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/c/{currencyCode}/last/{lastQuotations}");
            var parsed = await JsonSerializer.DeserializeAsync<BuyAskRateResponseDto>(response);
            var rates = parsed.Rates.Select(r => r.AskRate - r.BidRate).ToList();

            return rates.Max();
        }

        private static void ValidateQuotationsNumber(int lastQuotations)
        {
            if (lastQuotations < 1 || lastQuotations > 255)
            {
                throw new ArgumentException("The number of last quotations should be between 1-255");
            }
        }

        private static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentException("Please provide the currency code");
            }
        }
    }
}