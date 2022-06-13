namespace ExchangeRate.Models
{
    public class ExchangeRateRequest
    {
        public IList<DateTime> Dates { get; set; }

        public string BaseCurrency { get; set; }

        public string TargetCurrency { get; set; }
    }
}