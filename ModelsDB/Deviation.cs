using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class Deviation
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int? StepExecutionId { get; set; }

    public int ReportedBy { get; set; }

    public string ParameterName { get; set; } = null!;

    public decimal? PlannedValue { get; set; }

    public decimal? ActualValue { get; set; }

    public string? Severity { get; set; }

    public string? Description { get; set; }

    public DateTime? ReportedAt { get; set; }

    public int StatusId { get; set; }

    public virtual ProductionBatch Batch { get; set; } = null!;

    public virtual User ReportedByNavigation { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual BatchStepExecution? StepExecution { get; set; }
}
