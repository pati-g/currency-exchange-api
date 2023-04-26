﻿using System.Text.RegularExpressions;

namespace Shared
{
    public static class Validator
    {
        public static void ValidateQuotationsNumber(int lastQuotations)
        {
            if (lastQuotations < 1 || lastQuotations > 255)
            {
                throw new ArgumentException("The number of last quotations should be between 1-255");
            }
        }

        public static void ValidateCurrencyCode(string currencyCode)
        {
            if (string.IsNullOrEmpty(currencyCode))
            {
                throw new ArgumentException("Please provide the currency code");
            }
            if (!Regex.IsMatch(currencyCode, "[a-zA-Z]{3}"))
            {
                throw new ArgumentException("The currency code provided is invalid");
            }
        }
    }
}
