namespace APIClient
{
    public interface INBPClient
    {
        public Task<ExchangeRateResponseDto> GetExchangeRateAsync(string date, string currencyCode);
        public Task<ExchangeRateResponseDto> GetMaxAndMinExchangeRateAsync(string currencyCode, int lastQuotations);
        public Task<BuyAskRateResponseDto> GetAskBidMajorDifferenceAsync(string currencyCode, int lastQuotations);

    }
}
