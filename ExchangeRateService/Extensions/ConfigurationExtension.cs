using ExchangeRate.Shared.Settings;

namespace ExchangeRateService.Extensions
{
    public static class ConfigurationExtension
    {
        public static void AddConfigurationSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<ExchangeRateServiceSettings>(
                builder.Configuration.GetSection(ExchangeRateServiceSettings.Settings));
        }
    }
}
