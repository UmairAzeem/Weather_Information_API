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
    public class CountryInfo_Service : I_CountryInfo
    {
        private readonly DbWeatherConditionsContext _context;
        public CountryInfo_Service(DbWeatherConditionsContext context)
        {
            _context = context;
        }

        public async Task<List<CountryInfo>> GetCountryList()
        {
            var qCountryList = await _context.TblCountries.Select(a => new CountryInfo()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName,
                CountryCode = a.CountryCode,
                Iso2 = a.Iso2,
                Iso3 = a.Iso3,
                CapitalCity = a.CapitalCity,
                CountryPopulation = a.CountryPopulation.HasValue ? a.CountryPopulation.Value : 0,
                Continent = a.Continent

            }).OrderBy(a => a.CountryName).AsNoTracking().ToListAsync();

            return qCountryList;
        }

        public async Task<CountryInfo> CountryById(int countryId)
        {
            var qCountry = await _context.TblCountries.Where(a => a.CountryId == countryId).AsNoTracking().FirstOrDefaultAsync();

            CountryInfo res = new CountryInfo();

            if (qCountry != null)
            {
                res.CountryId = qCountry.CountryId;
                res.CountryName = qCountry.CountryName;
                res.Iso2 = qCountry.Iso2;
                res.Iso3 = qCountry.Iso3;
                res.CountryCode = qCountry.CountryCode;
                res.CapitalCity = qCountry.CapitalCity;
                res.CountryPopulation = qCountry.CountryPopulation.HasValue ? qCountry.CountryPopulation.Value : 0;
                res.Continent = qCountry.Continent;
            }

            return res;
        }

        public async Task<int> PostCountry(CountryInfo data)
        {
            if(data.CountryId == 0)
            {
                TblCountry country = new TblCountry();

                country.CountryName = data.CountryName;
                country.Iso2 = data.Iso2;
                country.Iso3 = data.Iso3;
                country.CountryCode = data.CountryCode;
                country.CapitalCity = data.CapitalCity;
                country.CountryPopulation = data.CountryPopulation;
                country.Continent = data.Continent;

                await _context.AddAsync(country);
                await _context.SaveChangesAsync();

                return country.CountryId;
            }

            return 0;
        }

        public async Task<int> PutCountry(CountryInfo data)
        {
            var qCountry = await _context.TblCountries.Where(a => a.CountryId == data.CountryId).AsNoTracking().FirstOrDefaultAsync();
            if (qCountry != null)
            {
                qCountry.CountryName = data.CountryName;
                qCountry.Iso2 = data.Iso2;
                qCountry.Iso3 = data.Iso3;
                qCountry.CountryCode = data.CountryCode;
                qCountry.CapitalCity = data.CapitalCity;
                qCountry.CountryPopulation = data.CountryPopulation;
                qCountry.Continent = data.Continent;

                await _context.SaveChangesAsync();

                return qCountry.CountryId;
            }

            return 0;
        }

        public async Task<List<CountryDropdown>> CountryDropdown()
        {
            var qCountry = await _context.TblCountries.Select(a => new CountryDropdown()
            {
                CountryId = a.CountryId,
                CountryName = a.CountryName
            }).OrderBy(a => a.CountryName).ToListAsync();

            return qCountry;
        }
    }
}
