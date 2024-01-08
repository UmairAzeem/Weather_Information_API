using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.I_BLService
{
    public interface I_Weather
    {
        public Task<WeatherInfo> LatestWeatherInfo(int cityId, string apiToken);
        public Task<int> SaveWeatherInformation(WeatherInfo data);
        public Task<List<WeatherInfo>> WeatherLogs(int cityId);
    }
}
