using Shared;

namespace Services
{
    public interface ICurrencyExchangeService
    {
        public Task<double> GetAverageExchangeRateAsync(string date, string currencyCode);
        public Task<MinMaxRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations);
        public Task<double> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations);
    }
}