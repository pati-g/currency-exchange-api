using APIClient;
using Shared;

namespace Services
{
    public class CurrencyExchangeService : ICurrencyExchangeService
    {
        private readonly INBPClient _client;

        public CurrencyExchangeService(INBPClient client)
        {
            _client = client;
        }

        public async Task<double> GetAverageExchangeRateAsync(DateOnly date, string currencyCode)
        {
            Validator.ValidateDate(date);
            Validator.ValidateCurrencyCode(currencyCode);
            var str = date.ToString("yyyy-MM-dd");
            var rateDto = await _client.GetExchangeRateByDateAsync(str, currencyCode);
            return rateDto.Rates[0].ExchangeRate;
        }

        public async Task<MinMaxRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations)
        {
            Validator.ValidateCurrencyCode(currencyCode);
            Validator.ValidateQuotationsNumber(lastQuotations);
            var rateDto = await _client.GetListOfExchangeRatesAsync(currencyCode, lastQuotations);
            var rates = rateDto.Rates.Select(r => r.ExchangeRate).ToList();
            return new MinMaxRateResponseDto { MinimalRate = rates.Min(), MaximumRate = rates.Max() };
        }
        public async Task<double> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations)
        {
            Validator.ValidateCurrencyCode(currencyCode);
            Validator.ValidateQuotationsNumber(lastQuotations);
            var rateDto = await _client.GetListOfAskBidRatesAsync(currencyCode, lastQuotations);
            var rates = rateDto.Rates.Select(r => r.AskRate - r.BidRate).ToList();
            return rates.Max();
        }
    }
}
