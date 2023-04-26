using NBPClient;

namespace NBPClientTests
{
    public class NBPClientTest
    {
        [Fact]
        public async Task GetExchangeRate_ReturnsCorrectValue()
        {
            var test = new NBPClient.NBPClient();
            var result = await test.GetAverageExchangeRateAsync("2023-04-20", "USD");
            Assert.True(result == 4.2024);
        }

        [Fact]
        public async Task GetMaxAndMin_ReturnsCorrectValue()
        {
            var test = new NBPClient.NBPClient();
            var result = await test.GetMaxAndMin("USD", 10);
            Assert.True(result == (4.2261, 4.1557));
        }

        [Fact]
        public async Task GetMajorDifference_ReturnsCorrectValue()
        {
            var test = new NBPClient.NBPClient();
            var result = await test.GetMajorDifference("USD", 10);
            Assert.True(result == 0.084999999999999964);
        }

        [Fact]
        public async Task GetMajorDifference_ThrowsException_WhenValueTooHighOrLow()
        {
            var test = new NBPClient.NBPClient();
            Assert.ThrowsAsync<ArgumentException>(async () => await test.GetMajorDifference("USD", 0));
            Assert.ThrowsAsync<ArgumentException>(async () => await test.GetMajorDifference("USD", 260));
        }

        [Fact]
        public async Task GetMajorDifference_ReturnsValue_WhenQuotationNumberIs1()
        {
            var test = new NBPClient.NBPClient();
            var result = await test.GetMajorDifference("USD", 1);
            Assert.True(result == 0.083400000000000141);
        }

    }
}