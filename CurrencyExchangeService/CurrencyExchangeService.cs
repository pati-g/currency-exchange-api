using APIClient;
using Shared;

namespace Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly INBPClient _client;
        // Date shouldn't be earlier than 2.01.2002
        // Single request shouldn't have span longer than 93 days

        public CurrencyExchangeService(INBPClient client)
        {
            _client = client;
        }

        public async Task<double> GetAverageExchangeRateAsync(string date, string currencyCode)
        {
            var rateDto = await _client.GetExchangeRateAsync(date, currencyCode);
            return rateDto.Rates[0].ExchangeRate;
        }

        public async Task<MinMaxRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations)
        {
            var rateDto = await _client.GetMaxAndMinExchangeRateAsync(currencyCode, lastQuotations);
            var rates = rateDto.Rates.Select(r => r.ExchangeRate).ToList();
            return new MinMaxRateResponseDto { MinimalRate = rates.Min(), MaximumRate = rates.Max() };
        }
        public async Task<double> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations)
        {
            var rateDto = await _client.GetAskBidMajorDifferenceAsync(currencyCode, lastQuotations);
            var rates = rateDto.Rates.Select(r => r.AskRate - r.BidRate).ToList();
            return rates.Max();
        }
    }
}
