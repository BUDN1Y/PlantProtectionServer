using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class Status
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public string? Description { get; set; }

    public string? Color { get; set; }

    public int? SortOrder { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsFinal { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<BatchStepExecution> BatchStepExecutions { get; set; } = new List<BatchStepExecution>();

    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    public virtual ICollection<LabTest> LabTests { get; set; } = new List<LabTest>();

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<RawMaterialBatch> RawMaterialBatches { get; set; } = new List<RawMaterialBatch>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual ICollection<StatusHistory> StatusHistoryNewStatuses { get; set; } = new List<StatusHistory>();

    public virtual ICollection<StatusHistory> StatusHistoryOldStatuses { get; set; } = new List<StatusHistory>();

    public virtual ICollection<TechMap> TechMaps { get; set; } = new List<TechMap>();
}
