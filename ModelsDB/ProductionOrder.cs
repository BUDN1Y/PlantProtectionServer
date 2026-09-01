using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class ProductionOrder
{
    public int Id { get; set; }

    public string OrderNumber { get; set; } = null!;

    public int ProductId { get; set; }

    public int RecipeId { get; set; }

    public int TechMapId { get; set; }

    public decimal PlannedQuantity { get; set; }

    public string Unit { get; set; } = null!;

    public DateOnly? PlannedStartDate { get; set; }

    public DateOnly? PlannedEndDate { get; set; }

    public int StatusId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual TechMap TechMap { get; set; } = null!;
}
