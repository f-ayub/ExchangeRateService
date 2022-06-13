using ExchangeRate.Models;
using ExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRateService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExchangeRateController : ControllerBase
    {

        private readonly ILogger<ExchangeRateController> _logger;
        private readonly IExchangeRateInfoService _exchangeRateInfoService;

        public ExchangeRateController(
            ILogger<ExchangeRateController> logger,
            IExchangeRateInfoService exchangeRateInfoService)
        {
            _logger = logger;
            _exchangeRateInfoService = exchangeRateInfoService;   
        }

        /// <summary>
        /// Gets min, max and average exchange rates for a given set of dates.
        /// </summary>
        /// <param name="exchangeRateRequest"></param>
        /// <returns></returns>
        [HttpGet("GetExchangeRateDetails")]
        public async Task<ExchangeRateResponse> Get(ExchangeRateRequest exchangeRateRequest)
        {
            return await _exchangeRateInfoService.GetExchangeRateInfo(exchangeRateRequest);
        }
    }
}