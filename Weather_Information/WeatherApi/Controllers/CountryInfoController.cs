using BusinessLogic.I_BLService;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;

namespace WeatherApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CountryInfoController : ControllerBase
    {
        private readonly I_CountryInfo _countryInfo;
        public CountryInfoController(I_CountryInfo countryInfo)
        {
            _countryInfo = countryInfo;
        }

        [HttpGet]
        public async Task<IActionResult> CountryList()
        {
            var res = new ApiResponse();

            var countryList = await _countryInfo.GetCountryList();
            if (countryList != null)
            {
                res.Data = countryList;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> CountryById(int country)
        {
            var res = new ApiResponse();

            var countryById = await _countryInfo.CountryById(country);
            if (countryById != null)
            {
                res.Data = countryById;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> PostCountry(CountryInfo data)
        {
            var res = new ApiResponse();

            var saveData = await _countryInfo.PostCountry(data);
            if (saveData != 0)
            {
                res.Data = saveData;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> PutCountry(CountryInfo data)
        {
            var res = new ApiResponse();

            var updateData = await _countryInfo.PutCountry(data);
            if (updateData != 0)
            {
                res.Data = updateData;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }

        [HttpGet]
        public async Task<IActionResult> CountryDropdown()
        {
            var res = new ApiResponse();

            var countryDropdown = await _countryInfo.CountryDropdown();
            if (countryDropdown != null)
            {
                res.Data = countryDropdown;
                res.Success = true;
                res.Message = "Success";
            }

            return Ok(res);
        }
    }
}
