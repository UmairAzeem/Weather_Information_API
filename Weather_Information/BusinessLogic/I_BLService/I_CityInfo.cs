using BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.I_BLService
{
    public interface I_CityInfo
    {
        public Task<List<CityList>> GetCityList(int country = 0);
        public Task<int> SaveCityData(CityData data);
        public Task<CityData> GetCityById(int cityId);
        public Task<int> UpdateCityData(CityData data);
    }
}
