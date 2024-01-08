using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class CityInfo
    {
    }

    public class CityData
    {
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CountryId { get; set; }
    }

    public class CityList : CityData
    {
        public string? CountryName { get; set; }
        public string? Iso2 { get; set; }
        public string? Iso3 { get; set; }
        public string? CountryCode { get; set; }
        public string? CapitalCity { get; set; }
        public int CountryPopulation { get; set; }
        public string? Continent { get; set; }
    }
}
