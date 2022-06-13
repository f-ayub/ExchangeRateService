using ExchangeRate.Services;
using ExchangeRate.Services.ExchangeRateApi;
using ExchangeRate.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using ExchangeRate.Shared.Exceptions;
using ExchangeRate.Shared.Settings;
using Microsoft.Extensions.Options;

namespace ExchangeRateService.Tests
{
    public class Tests
    {
        private ExchangeRateInfoService _exchangeRateInfoService;
        private IExchangeRateApiServiceClient _exchangeRateApiServiceClient;


        [Test]
        public void ValidRequestReturnsExpectedRateDetails()
        {
            //Arrange
            _exchangeRateApiServiceClient = Substitute.For<IExchangeRateApiServiceClient>();
            _exchangeRateInfoService = new ExchangeRateInfoService(_exchangeRateApiServiceClient);
            var request = GivenValidRequest();
            GivenExchangeRateApiResponse();

            // Act
            var result = _exchangeRateInfoService.GetExchangeRateInfo(request).Result;

            // Assert
            Assert.IsNotNull(result);
            // Minimum 
            Assert.IsTrue(result.MinimumRate.Result == 0.923858m && result.MinimumRate.Date == "2018-04-13");
            // Maximum 
            Assert.IsTrue(result.MaximumRate.Result == 0.938134m && result.MaximumRate.Date == "2018-04-01");
            // Average 
            Assert.IsTrue(result.AverageRate == 0.931427m);
        }

        [Test]
        public async Task InValidRequestThrowsException()
        {
            //Arrange
            _exchangeRateApiServiceClient = new ExchangeRateApiServiceClient(Options.Create(new ExchangeRateServiceSettings { ExchangeRateBaseUri = "https://api.exchangerate.host/" }));
            _exchangeRateInfoService = new ExchangeRateInfoService(_exchangeRateApiServiceClient);
            var request = GivenInValidRequest();

            // Assert
            Assert.ThrowsAsync<HttpResponseException>(async () => await _exchangeRateInfoService.GetExchangeRateInfo(request));
        }

        private ExchangeRateRequest GivenValidRequest()
        {
            // Given Request
            return new ExchangeRateRequest
            {
                BaseCurrency = "SEK",
                TargetCurrency = "NOK",
                Dates = new List<DateTime> { new DateTime(2018, 4, 1), new DateTime(2018, 4, 12), new DateTime(2018, 4, 13) }
            };
        }

        private void GivenExchangeRateApiResponse()
        {
            // Response From ExchangeRate API
            _exchangeRateApiServiceClient.GetExchangeRate(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>("2018-04-01"))
                .Returns(new ExchangeRateModel { Date = "2018-04-01", Result = 0.938134m });

            _exchangeRateApiServiceClient.GetExchangeRate(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>("2018-04-12"))
                .Returns(new ExchangeRateModel { Date = "2018-04-12", Result = 0.932289m });

            _exchangeRateApiServiceClient.GetExchangeRate(Arg.Any<string>(), Arg.Any<string>(), Arg.Is<string>("2018-04-13"))
                .Returns(new ExchangeRateModel { Date = "2018-04-13", Result = 0.923858m });
        }

        private ExchangeRateRequest GivenInValidRequest()
        {
            // Given Request
            return new ExchangeRateRequest
            {
                BaseCurrency = "ABC",
                TargetCurrency = "ABC",
                Dates = new List<DateTime> { new DateTime(2018, 4, 1), new DateTime(2018, 4, 12), new DateTime(2018, 4, 13) }
            };
        }
    }
}