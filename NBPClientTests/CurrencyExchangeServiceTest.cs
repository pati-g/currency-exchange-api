using APIClient;
using Moq;
using Services;
using Shared;

namespace NBPClientTests
{
    public class CurrencyExchangeServiceTest
    {
        private readonly ICurrencyExchangeService _service;
        private readonly INBPClient _client;

        public CurrencyExchangeServiceTest()
        {
            _client = new NBPClient();
            _service = new CurrencyExchangeService(_client);
        }

        [Fact]
        public async Task GetExchangeRate_ReturnsCorrectValue()
        {
            var result = await _service.GetAverageExchangeRateAsync("2023-04-20", "USD");
            Assert.True(result == 4.2024);
        }

        [Fact]
        public async Task GetMaxAndMin_ReturnsCorrectValue()
        {
            var result = await _service.GetMaxAndMinExchangeRateAsync("USD", 10);
            var expected = new MinMaxRateResponseDto { MinimalRate = 4.1557, MaximumRate = 4.2261 };
            Assert.True(result.MinimalRate == expected.MinimalRate);
            Assert.True(result.MaximumRate == expected.MaximumRate);
        }

        [Fact]
        public async Task GetMajorDifference_ReturnsCorrectValue()
        {
            var result = await _service.GetAskBidMajorDifferenceAsync("USD", 10);
            Assert.True(result == 0.084999999999999964);
        }

        [Fact]
        public async Task GetMajorDifference_ThrowsException_WhenValueTooHighOrLow()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.GetAskBidMajorDifferenceAsync("USD", 0));
            Assert.ThrowsAsync<ArgumentException>(async () => await _service.GetAskBidMajorDifferenceAsync("USD", 260));
        }

        [Fact]
        public async Task GetMajorDifference_ReturnsValue_WhenQuotationNumberIs1()
        {
            var result = await _service.GetAskBidMajorDifferenceAsync("USD", 1);
            Assert.True(result == 0.083400000000000141);
        }

    }
}