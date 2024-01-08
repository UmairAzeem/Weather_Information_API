using System;
using System.Collections.Generic;

namespace DataAccessEF.DbEfModels;

public partial class TblWeatherLog
{
    public int WeatherLogsId { get; set; }

    public decimal? TempC { get; set; }

    public decimal? TempF { get; set; }

    public decimal? WindKph { get; set; }

    public string? WindDir { get; set; }

    public decimal? Humidity { get; set; }

    public int? CityId { get; set; }

    public string? Icon { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual TblCity? City { get; set; }
}
