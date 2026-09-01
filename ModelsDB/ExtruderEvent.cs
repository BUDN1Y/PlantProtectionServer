using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class ExtruderEvent
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public string? Zone { get; set; }

    public string ParameterName { get; set; } = null!;

    public decimal? Value { get; set; }

    public string? Status { get; set; }

    public DateTime? EventTime { get; set; }

    public string? Description { get; set; }

    public virtual ProductionBatch Batch { get; set; } = null!;
}
