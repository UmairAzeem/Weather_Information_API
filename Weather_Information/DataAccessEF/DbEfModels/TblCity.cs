using System;
using System.Collections.Generic;

namespace DataAccessEF.DbEfModels;

public partial class TblCity
{
    public int CityId { get; set; }

    public string? CityName { get; set; }

    public int? CountryId { get; set; }

    public int? CityPopulation { get; set; }

    public DateTime? CreatedOn { get; set; }

    public DateTime? LastUpdateOn { get; set; }

    public virtual TblCountry? Country { get; set; }

    public virtual ICollection<TblWeatherLog> TblWeatherLogs { get; set; } = new List<TblWeatherLog>();
}
