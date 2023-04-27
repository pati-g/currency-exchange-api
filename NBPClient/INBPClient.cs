namespace APIClient
{
    public interface INBPClient
    {
        public Task<ExchangeRateResponseDto> GetExchangeRateByDateAsync(string date, string currencyCode);
        public Task<ExchangeRateResponseDto> GetListOfExchangeRatesAsync(string currencyCode, int lastQuotations);
        public Task<BuyAskRateResponseDto> GetListOfAskBidRatesAsync(string currencyCode, int lastQuotations);

    }
}
