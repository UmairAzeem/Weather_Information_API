using ExternalServices.OpenWeatherMap.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServices.OpenWeatherMap
{
    public class OpenWeatherMapApi
    {
        public async Task<WeatherApiRes> GetData(string cityName, string apiToken)
        {
            string url = $"https://api.weatherapi.com/v1/current.json?q={cityName}&key={apiToken}";
            string json = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (HttpWebResponse result = (HttpWebResponse)request.GetResponse())
            using (Stream stream = result.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                json = reader.ReadToEnd();
            }

            dynamic jsonObj = null;
            jsonObj = JsonConvert.DeserializeObject(json);

            WeatherApiRes res = new WeatherApiRes();

            res.CityName = jsonObj.location.name;
            res.TempC = jsonObj.current.temp_c;
            res.TempF = jsonObj.current.temp_f;
            res.WindKph = jsonObj.current.wind_kph;
            res.WindDir = jsonObj.current.wind_dir;
            res.Humidity = jsonObj.current.humidity;
            res.LastUpdate = jsonObj.current.last_updated;
            res.Icon = jsonObj.current.condition.icon;

            return res;
        }
        
    }
}
