using BusinessLogic.I_BLService;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CityInfoController : ControllerBase
    {
        private readonly I_CityInfo _cityInfo;
        public CityInfoController(I_CityInfo cityInfo)
        {
            _cityInfo = cityInfo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCity(int country = 0)
        {
            //generic API response
            var res = new ApiResponse();

            //Get city list. If country id = 0 it return all cities
            var getCity = await _cityInfo.GetCityList(country);
            if (getCity.Count != 0)
            {
                res.Data = getCity;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCityData(CityData data)
        {
            var res = new ApiResponse();

            //Save city information
            var cityId = await _cityInfo.SaveCityData(data);
            if (cityId != 0)
            {
                res.Data = cityId;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCityData(CityData data)
        {
            var res = new ApiResponse();

            var cityId = await _cityInfo.UpdateCityData(data);
            if (cityId != 0)
            {
                res.Data = cityId;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> GetCityById(int city)
        {
            var res = new ApiResponse();

            var cityById = await _cityInfo.GetCityById(city);
            if (cityById != null)
            {
                res.Data = cityById;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }
    }
}
