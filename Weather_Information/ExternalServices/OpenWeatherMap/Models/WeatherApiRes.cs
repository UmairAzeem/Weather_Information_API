using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServices.OpenWeatherMap.Models
{
    public class WeatherApiRes
    {
        public decimal? TempC { get; set; }

        public decimal? TempF { get; set; }

        public decimal? WindKph { get; set; }

        public string? WindDir { get; set; }

        public decimal? Humidity { get; set; }
        public string? Icon { get; set; }

        public string? CityName { get; set; }

        public DateTime? LastUpdate { get; set; }
    }
}
