using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPIVersioning.Controllers
{
    
    [ApiController]

    [Route("api/[Controller]")]

    //[Route("api/v{version:apiVersion}/[Controller]")]
    //example https://localhost:44363/api/v1.0/weatherforecast/getreport
    //example https://localhost:44363/api/v2.0/weatherforecast/getreport
    //example https://localhost:44363/api/v1/weatherforecast/getreport
    //example https://localhost:44363/api/v2/weatherforecast/getreport

    //[ApiVersion("1.0")]

    // response header api-deprecated-versions:1.0: to let the clinet know about the versions of API is Deprecated
    [ApiVersion("1.0", Deprecated =true)]
    [ApiVersion("2.0")]
    
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        // examplae https://localhost:44363/api/weatherforecast/getreport
        [HttpGet("getreport")]        
        public IEnumerable<WeatherForecast> GetWeatherForecast()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        // examplae https://localhost:44363/api/weatherforecast/getreport?api-version=2.0    -- by default query string parameter version
        [HttpGet("getreport")]
        [MapToApiVersion("2.0")]
        public IEnumerable<WeatherForecastV1> GetWeatherForecastV1()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecastV1
            {
                ApiVersion = "Done by Pandey",
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
                
                
            })
            .ToArray();
        }
    }
}
