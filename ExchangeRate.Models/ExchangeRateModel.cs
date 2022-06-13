using Newtonsoft.Json;

namespace ExchangeRate.Models
{
    public class ExchangeRateModel : IComparable<ExchangeRateModel>
    {
        public string Date { get; set; }        
        public decimal Result { get; set; }
        public int CompareTo(ExchangeRateModel exchangeRate)
        {
            return Result.CompareTo(exchangeRate.Result);
        }
    }
}