using ExchangeRate.Services;
using ExchangeRate.Services.ExchangeRateApi;
using ExchangeRateService.Filters;

namespace ExchangeRateService.Extensions
{
    public static class DependenciesExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExchangeRateInfoService, ExchangeRateInfoService>();
            services.AddScoped<IExchangeRateApiServiceClient, ExchangeRateApiServiceClient>();
            services.AddControllers(options =>
            {
                options.Filters.Add<HttpResponseExceptionFilter>();
            });
        }
    }
}
