using BusinessLogic.I_BLService;
using BusinessLogic.Models;
using DataAccessEF.DbEfModels;
using ExternalServices.OpenWeatherMap;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BLService
{
    public class Weather_Service : I_Weather
    {
        private readonly DbWeatherConditionsContext _context;
        private readonly OpenWeatherMapApi _weatherApi;
        public Weather_Service(DbWeatherConditionsContext context, OpenWeatherMapApi weatherApi)
        {
            _context = context;
            _weatherApi = weatherApi;
        }
        public async Task<WeatherInfo> LatestWeatherInfo(int cityId, string apiToken)
        {
            var qCity = await _context.TblCities.Where(a => a.CityId == cityId).FirstOrDefaultAsync();
            if(qCity != null)
            {
                WeatherInfo weatherInfo = new WeatherInfo();

                var data = await _weatherApi.GetData(qCity.CityName, apiToken);

                weatherInfo.CityId = qCity.CityId;
                weatherInfo.CityName = data.CityName;
                weatherInfo.TempC = data.TempC;
                weatherInfo.TempF = data.TempF;
                weatherInfo.WindKph = data.WindKph;
                weatherInfo.WindDir = data.WindDir;
                weatherInfo.Humidity = data.Humidity;
                weatherInfo.LastUpdate = data.LastUpdate.Value.ToString("dd-MMM-yyyy");
                weatherInfo.Icon = data.Icon;

                return weatherInfo;
            }

            return null;
        }

        public async Task<int> SaveWeatherInformation(WeatherInfo data)
        {
            TblWeatherLog weatherLog = new TblWeatherLog();
            weatherLog.CityId = data.CityId;
            weatherLog.TempC = data.TempC;
            weatherLog.TempF = data.TempF;
            weatherLog.Humidity= data.Humidity;
            weatherLog.WindDir = data.WindDir;
            weatherLog.WindKph= data.WindKph;
            weatherLog.CreatedDate = DateTime.Now;
            weatherLog.Icon = data.Icon;

            await _context.AddAsync(weatherLog);
            await _context.SaveChangesAsync();

            return weatherLog.WeatherLogsId;
        }

        public async Task<List<WeatherInfo>> WeatherLogs(int cityId)
        {
            var weatherLog = await _context.TblWeatherLogs.Include(a => a.City).Where(x => x.CityId == cityId).Select(a => new WeatherInfo()
            {
                CityId = a.CityId.Value,
                CityName = a.City.CityName,
                TempC = a.TempC,
                TempF = a.TempF,
                Humidity = a.Humidity,
                WindDir = a.WindDir,
                WindKph = a.WindKph,
                Icon = a.Icon,
                LastUpdate = a.CreatedDate.Value.ToString("dd-MMM-yyyy")
            }).AsNoTracking().ToListAsync();

            return weatherLog;
        }
    }
}
