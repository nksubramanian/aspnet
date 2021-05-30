using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
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


        [HttpGet("GetHeaderData")]
        public string GetHeaderData([FromHeader]string developer, [FromHeader]string dev)
        {
            return $"Name= {developer} " + $" Name= {dev}";
        }


        [HttpPost("/reading-body")]
        public string ReadBody([FromBody] RequestBody requestBody)
        {
            return $"The header value is name:{requestBody.Name} city:{requestBody.City}";
        }

        public class RequestBody
        {
            public string Name { get; set; }
            public string City { get; set; }
        }


        [HttpGet("{id}/{first}")]
        public string ReadPath(string id, string first)
        {
            return $"The header value is name:{id} city:{first}";
        }

        [HttpGet("reading-query")]
        public string ReadQuery([FromQuery(Name = "name")] string name, [FromQuery(Name = "age")] int age)
        {
            age++;
            return $"The header value is name:{name} age:{age}";
        }



    }
}
