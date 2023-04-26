﻿using Microsoft.Extensions.Logging;
using System.Globalization;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace NBPClient
{
    public class NBPClient
    {
        private static readonly HttpClient _client = new();
        private const string _base = "http://api.nbp.pl/api/exchangerates/rates";

        // Date shouldn't be earlier than 2.01.2022
        // Single request shouldn't have span longer than 93 days

        public async Task<double> GetAverageExchangeRateAsync(string date, string currencyCode)
        {
            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/{date}");
            var parsed = JsonSerializer.Deserialize<NBPCurrencyResponseDto>(response);
            return parsed.rates[0].mid;
        }

        public async Task<(double max, double min)> GetMaxAndMin(string currencyCode, int lastQuotations)
        {
            //http://api.nbp.pl/api/exchangerates/rates/a/gbp/last/10

            ValidateQuotationsNumber(lastQuotations);
            ValidateCurrencyCode(currencyCode);

            var response = await _client.GetStreamAsync($"{_base}/a/{currencyCode}/last/{lastQuotations}");
            var rates = JsonSerializer.Deserialize<NBPCurrencyResponseDto>(response).rates.Select(r => r.mid).ToList();
            return (rates.Max(), rates.Min());
        }

        private static void ValidateQuotationsNumber(int lastQuotations)
        {
            if (lastQuotations < 0 || lastQuotations > 255)
            {
                throw new ArgumentException("The number of last quotations should be between 1-255");
            }
        }

        private static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentException("Please provide the currency code");
            }
        }
    }
}