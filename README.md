ExchangeRateService

Developed using .NET 6.0 in Visual Studio 2022

To run the code open solution in Visual Studio 2022 and run ExchangeRateService project.

API Endpoint URL : http://localhost:5164/ExchangeRate/GetExchangeRateDetails

Http Method : GET

Sample Request Body :

```json
{
    "Dates":[
        "2018-03-01",
        "2018-02-15",
        "2018-02-01",
        "2019-03-01",
        "2020-03-23"
    ],
    "BaseCurrency": "SEK",
    "TargetCurrency": "NOK"
}
```

Sample Response :

```json
{
    "minimumRate": {
        "date": "2019-03-01",
        "result": 0.926358
    },
    "maximumRate": {
        "date": "2020-03-23",
        "result": 1.128491
    },
    "averageRate": 0.9877952321428571428571428571
}
```
