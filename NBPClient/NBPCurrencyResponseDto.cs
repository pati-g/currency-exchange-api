using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBPClient
{
    public class NBPCurrencyResponseDto
    {
        public string table { get; set; }
        public string currency { get; set; } = string.Empty;
        public string code { get; set; } = string.Empty;
        public List<Rate> rates { get; set; }
    }

    public class Rate
    {
        public string no { get; set; } = string.Empty;
        public string effectiveDate { get; set; } = string.Empty;
        public double mid { get; set; }
    }
}
