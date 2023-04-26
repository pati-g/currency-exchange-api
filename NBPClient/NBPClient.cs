using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace NBPClient
{
    public class NBPClient
    {
        private static readonly HttpClient _client = new();
        private const string _base = "http://api.nbp.pl/api";

        // Date shouldn't be earlier than 2.01.2022
        // Single request shouldn't have span longer than 93 days

        public async Task<double> GetAverageExchangeRateAsync(string date, string currencyCode)
        {
            var response = await _client.GetStringAsync($"{_base}/exchangerates/rates/a/{currencyCode}/{date}");
            var rateAsString = response.Substring(response.LastIndexOf(":")+1, 6);
            return double.Parse(rateAsString, CultureInfo.InvariantCulture);
        }

    }
}