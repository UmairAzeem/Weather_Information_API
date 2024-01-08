using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.I_BLService
{
    public interface I_CountryInfo
    {
        public Task<List<CountryInfo>> GetCountryList();
        public Task<int> PostCountry(CountryInfo data);
        public Task<int> PutCountry(CountryInfo data);
        public Task<CountryInfo> CountryById(int countryId);
        public Task<List<CountryDropdown>> CountryDropdown();
    }
}
