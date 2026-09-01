using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class ProductionBatch
{
    public int Id { get; set; }

    public string BatchNumber { get; set; } = null!;

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int RecipeVersionId { get; set; }

    public int TechMapVersionId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int StatusId { get; set; }

    public int? CurrentStepId { get; set; }

    public int? ExtruderProgramId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? LabDecision { get; set; }

    public string? LabComment { get; set; }

    public virtual ICollection<BatchStepExecution> BatchStepExecutions { get; set; } = new List<BatchStepExecution>();

    public virtual ICollection<BatchUsedMaterial> BatchUsedMaterials { get; set; } = new List<BatchUsedMaterial>();

    public virtual TechMapStep? CurrentStep { get; set; }

    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    public virtual ICollection<ExtruderEvent> ExtruderEvents { get; set; } = new List<ExtruderEvent>();

    public virtual ProductionOrder Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Recipe RecipeVersion { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual TechMap TechMapVersion { get; set; } = null!;
}
