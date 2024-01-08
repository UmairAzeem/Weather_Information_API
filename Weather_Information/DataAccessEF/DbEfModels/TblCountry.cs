using System;
using System.Collections.Generic;

namespace DataAccessEF.DbEfModels;

public partial class TblCountry
{
    public int CountryId { get; set; }

    public string? CountryName { get; set; }

    public string? Iso2 { get; set; }

    public string? Iso3 { get; set; }

    public string? CountryCode { get; set; }

    public string? CapitalCity { get; set; }

    public int? CountryPopulation { get; set; }

    public string? Continent { get; set; }

    public virtual ICollection<TblCity> TblCities { get; set; } = new List<TblCity>();
}
