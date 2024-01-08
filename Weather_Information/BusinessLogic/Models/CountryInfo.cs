using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
    public class CountryInfo
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? Iso2 { get; set; }
        public string? Iso3 { get; set; }
        public string? CountryCode { get; set; }
        public string? CapitalCity { get; set; }
        public int CountryPopulation { get; set; }
        public string? Continent { get; set; }
    }

    public class CountryDropdown
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
    }
}
