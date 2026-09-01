using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class LabTestParameter
{
    public int Id { get; set; }

    public int LabTestId { get; set; }

    public string ParameterName { get; set; } = null!;

    public string? Unit { get; set; }

    public decimal? NormMin { get; set; }

    public decimal? NormMax { get; set; }

    public decimal? ActualValue { get; set; }

    public string? ResultStatus { get; set; }

    public string? Notes { get; set; }

    public virtual LabTest LabTest { get; set; } = null!;
}
