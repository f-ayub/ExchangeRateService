using ExchangeRate.Models;
using ExchangeRate.Shared.Exceptions;
using ExchangeRate.Shared.Settings;
using Microsoft.Extensions.Options;
using RestSharp;

namespace ExchangeRate.Services.ExchangeRateApi
{
    public interface IExchangeRateApiServiceClient
    {
        public Task<ExchangeRateModel> GetExchangeRate(string baseCurrency, string targetCurrency, string date);
    }
    public class ExchangeRateApiServiceClient : IExchangeRateApiServiceClient   
    {
        private readonly ExchangeRateServiceSettings _exchangeRateServiceSettings;
        public ExchangeRateApiServiceClient(IOptions<ExchangeRateServiceSettings> exchangeRateServiceSettings)
        {
            _exchangeRateServiceSettings = exchangeRateServiceSettings.Value;
        }
        public async Task<ExchangeRateModel> GetExchangeRate(string baseCurrency, string targetCurrency, string date)
        {
            var relativeUri = new Uri("convert", UriKind.Relative);
            var address = new Uri(new Uri(_exchangeRateServiceSettings.ExchangeRateBaseUri), relativeUri);
            var client = new RestClient(address);

            var request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddQueryParameter("from", baseCurrency);
            request.AddQueryParameter("to", targetCurrency);
            request.AddQueryParameter("date", date);
            var result = await client.ExecuteAsync<ExchangeRateModel>(request);

            if (result.IsSuccessful && result.Data != default)
            {
                return result.Data;
            }
            else
            {
                throw new HttpResponseException((int)result.StatusCode, "Invalid rate received for given parameters");
            }
        }
    }
}
