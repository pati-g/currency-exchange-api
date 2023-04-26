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
    }
}