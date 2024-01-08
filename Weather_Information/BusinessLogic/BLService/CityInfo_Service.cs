using BusinessLogic.I_BLService;
using BusinessLogic.Models;
using DataAccessEF.DbEfModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.BLService
{
    public class CityInfo_Service : I_CityInfo
    {
        private readonly DbWeatherConditionsContext _context;
        public CityInfo_Service(DbWeatherConditionsContext context)
        {
            _context = context;
        }

        public async Task<List<CityList>> GetCityList(int country = 0)
        {
            var qCityQuery = _context.TblCities.AsQueryable();

            if (country != 0)
            {
                qCityQuery = qCityQuery.Where(x => x.CountryId == country).AsQueryable();
            }

            var qCityList = await qCityQuery.Include(a => a.Country).Select(a => new CityList()
            {
                CityId = a.CityId,
                CityName = a.CityName,
                CountryId = a.CountryId.ToString(),
                CountryName = a.Country.CountryName,
                CountryCode = a.Country.CountryCode,
                Iso2 = a.Country.Iso2,
                Iso3 = a.Country.Iso3,
                CapitalCity = a.Country.CapitalCity,
                CountryPopulation = a.Country.CountryPopulation.HasValue ? a.Country.CountryPopulation.Value : 0,
                Continent = a.Country.Continent

            }).AsNoTracking().ToListAsync();

            return qCityList;
        }

        public async Task<int> SaveCityData(CityData data)
        {
            if(data.CityId == 0)
            {
                TblCity city = new TblCity();
                city.CityName = data.CityName;
                city.CountryId = Convert.ToInt32(data.CountryId);

                await _context.AddAsync(city);
                await _context.SaveChangesAsync();

                return city.CityId;
            }

            return 0;
        }

        public async Task<int> UpdateCityData(CityData data)
        {
            if (data.CityId != 0)
            {
                var qCity = await _context.TblCities.Where(a => a.CityId == data.CityId).FirstOrDefaultAsync();

                if (qCity != null)
                {
                    qCity.CityName = data.CityName;
                    qCity.CountryId = Convert.ToInt32(data.CountryId);

                    await _context.SaveChangesAsync();

                    return data.CityId;
                }
            }

            return 0;
        }

        public async Task<CityData> GetCityById(int cityId)
        {
            CityData res = new CityData();
            if (cityId != 0)
            {
                var qCityData = await _context.TblCities.Where(x => x.CityId == cityId).FirstOrDefaultAsync();
                if (qCityData != null)
                {
                    res.CityId = qCityData.CityId;
                    res.CountryId = qCityData.CountryId.ToString();
                    res.CityName = qCityData.CityName;
                }
            }

            return res;
        }
    }
}
