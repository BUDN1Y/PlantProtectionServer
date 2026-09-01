using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class BatchStepExecution
{
    public int Id { get; set; }

    public int BatchId { get; set; }

    public int TechStepId { get; set; }

    public int StatusId { get; set; }

    public DateTime? StartedAt { get; set; }

    public DateTime? FinishedAt { get; set; }

    public int? StartedBy { get; set; }

    public int? FinishedBy { get; set; }

    public decimal? ActualTemp { get; set; }

    public decimal? ActualPressure { get; set; }

    public int? ActualTime { get; set; }

    public string? Comment { get; set; }

    public virtual ProductionBatch Batch { get; set; } = null!;

    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    public virtual User? FinishedByNavigation { get; set; }

    public virtual User? StartedByNavigation { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual TechMapStep TechStep { get; set; } = null!;
}
