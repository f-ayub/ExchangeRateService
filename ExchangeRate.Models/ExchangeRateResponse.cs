namespace ExchangeRate.Models
{
    public class ExchangeRateResponse
    {
        public ExchangeRateModel MinimumRate { get; set; }
        public ExchangeRateModel MaximumRate { get; set; }
        public decimal AverageRate { get; set; }
    }
}
