using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RedisDemo.Extensions;
using System.Threading.Tasks;

namespace RedisDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IDistributedCache _cache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IDistributedCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpGet("/GetWeather")]
        public async Task<ActionResult<WeatherForecast>> Get()
        {
            return Ok(await _cache.GetRecordAsync<WeatherForecast>("12"));
        }

        [HttpPost("/PostWeather")]
        public async Task<ActionResult> Post([FromQuery]WeatherForecast foreCast)
        {
            await _cache.SetRecordAsync<WeatherForecast>("12", foreCast);

            return Ok();
        }
    }
}
