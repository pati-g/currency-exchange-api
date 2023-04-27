using Shared;
using System.Text.Json;

namespace APIClient
{
    public class NBPClient : INBPClient
    {
        private static readonly HttpClient _client = new();
        private const string _base = "http://api.nbp.pl/api/exchangerates/rates";

        public async Task<ExchangeRateResponseDto> GetExchangeRateByDateAsync(string date, string currencyCode)
        {
            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/{date}");
            return await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
        }

        public async Task<ExchangeRateResponseDto> GetListOfExchangeRatesAsync(string currencyCode, int lastQuotations)
        {
            Validator.ValidateQuotationsNumber(lastQuotations);
            Validator.ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/last/{lastQuotations}");
            return await JsonSerializer.DeserializeAsync<ExchangeRateResponseDto>(response);
        }

        public async Task<BuyAskRateResponseDto> GetListOfAskBidRatesAsync(string currencyCode, int lastQuotations)
        {
            Validator.ValidateQuotationsNumber(lastQuotations);
            Validator.ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/c/{currencyCode}/last/{lastQuotations}");
            return await JsonSerializer.DeserializeAsync<BuyAskRateResponseDto>(response);
        }
    }
}