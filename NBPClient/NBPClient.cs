using System.Text.Json;

namespace APIClient
{
    public class NBPClient : INBPClient
    {
        private static readonly HttpClient _client = new();
        private const string _base = "http://api.nbp.pl/api/exchangerates/rates";

        public async Task<ExchangeRateResponseDto> GetExchangeRateAsync(string date, string currencyCode)
        {
            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/{date}");
            return await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
        }

        public async Task<ExchangeRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations)
        {
            ValidateQuotationsNumber(lastQuotations);
            ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/last/{lastQuotations}");
            return await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
        }

        public async Task<BuyAskRateResponseDto> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations)
        {
            ValidateQuotationsNumber(lastQuotations);
            ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/c/{currencyCode}/last/{lastQuotations}");
            return await JsonSerializer.DeserializeAsync<BuyAskRateResponseDto>(response);
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