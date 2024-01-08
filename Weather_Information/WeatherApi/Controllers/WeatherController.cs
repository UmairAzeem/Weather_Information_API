using BusinessLogic.I_BLService;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly I_Weather _Weather;
        private readonly IConfiguration _configuration;
        public WeatherController(I_Weather Weather, IConfiguration configuration)
        {
            _Weather = Weather;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> WeatherInfo(int city)
        {
            var res = new ApiResponse();

            string weatherApiKey = _configuration["WeatherApiKey"];

            var weatherData = await _Weather.LatestWeatherInfo(city, weatherApiKey);
            if (weatherData != null)
            {
                res.Data = weatherData;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> SaveWeatherInformation(WeatherInfo data)
        {
            var res = new ApiResponse();

            var saveWeatherInfo = await _Weather.SaveWeatherInformation(data);
            if (saveWeatherInfo != 0)
            {
                res.Data = saveWeatherInfo;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> WeatherLogs(int city)
        {
            var res = new ApiResponse();

            var weatherLogs = await _Weather.WeatherLogs(city);
            if (weatherLogs.Count > 0)
            {
                res.Data = weatherLogs;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }
    }
}
