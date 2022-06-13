using ExchangeRate.Models;
using ExchangeRate.Services.ExchangeRateApi;

namespace ExchangeRate.Services
{
    public interface IExchangeRateInfoService
    {
        public Task<ExchangeRateResponse> GetExchangeRateInfo(ExchangeRateRequest exchangeRateRequest);
    }
    public class ExchangeRateInfoService : IExchangeRateInfoService
    {
        private readonly IExchangeRateApiServiceClient _exchangeRateApiServiceClient;
        public ExchangeRateInfoService(
            IExchangeRateApiServiceClient exchangeRateApiServiceClient)            
        {
            _exchangeRateApiServiceClient = exchangeRateApiServiceClient;   
        }

        public async Task<ExchangeRateResponse> GetExchangeRateInfo(ExchangeRateRequest exchangeRateRequest)
        {
            var exchangeRatesInfo = new List<ExchangeRateModel>();
            var tasks = new List<Task>();

            foreach (var date in exchangeRateRequest.Dates)
            {
                async Task func()
                {
                    exchangeRatesInfo.Add(await _exchangeRateApiServiceClient.GetExchangeRate(
                        exchangeRateRequest.BaseCurrency,
                        exchangeRateRequest.TargetCurrency,
                        date.ToString("yyyy-MM-dd"))
                    );
                }
                // Add tasks for calling API mulitple times in parallel
                tasks.Add(func());
            }

            await Task.WhenAll(tasks);

            return new ExchangeRateResponse
            {
                MinimumRate = exchangeRatesInfo.Min(),
                MaximumRate = exchangeRatesInfo.Max(),
                AverageRate = exchangeRatesInfo.Average(x => x.Result)
            };
        }
    }
}