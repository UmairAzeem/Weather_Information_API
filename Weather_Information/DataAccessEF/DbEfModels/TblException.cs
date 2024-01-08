using System;
using System.Collections.Generic;

namespace DataAccessEF.DbEfModels;

public partial class TblException
{
    public int ExceptionId { get; set; }

    public int? StatusCode { get; set; }

    public string? ControllerName { get; set; }

    public string? ActionName { get; set; }

    public string? StackTrace { get; set; }

    public string? ExceptionMessage { get; set; }

    public DateTime? ExceptionDate { get; set; }
}
