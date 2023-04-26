namespace CurrencyExchangeAPI
{
    public class CurrencyRequestDto
    {
        public string CurrencyCode { get; set; }
        public DateOnly Date { get; set; }
    }
}
