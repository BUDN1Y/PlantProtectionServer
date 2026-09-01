using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class TechMapStep
{
    public int Id { get; set; }

    public int TechMapId { get; set; }

    public int StepNumber { get; set; }

    public string Name { get; set; } = null!;

    public string StepType { get; set; } = null!;

    public string? Instruction { get; set; }

    public decimal? PlannedTemp { get; set; }

    public decimal? PlannedPressure { get; set; }

    public int? PlannedTimeMin { get; set; }

    public int? PlannedTimeMax { get; set; }

    public decimal? ToleranceTempMin { get; set; }

    public decimal? ToleranceTempMax { get; set; }

    public decimal? TolerancePressureMin { get; set; }

    public decimal? TolerancePressureMax { get; set; }

    public bool? IsMandatory { get; set; }

    public virtual ICollection<BatchStepExecution> BatchStepExecutions { get; set; } = new List<BatchStepExecution>();

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual TechMap TechMap { get; set; } = null!;
}
