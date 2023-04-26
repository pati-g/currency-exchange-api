using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPClient
{
    public class NBPCurrencyResponseDto
    {
        public string Table { get; set; }
        public string Currency { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public List<Dictionary<string, string>> Rates { get; set; }
    }

    public class Rate
    {
        public string No { get; set; } = string.Empty;
        public string EffectiveDate { get; set; } = string.Empty;
        public double Mid { get; set; }
    }
}
