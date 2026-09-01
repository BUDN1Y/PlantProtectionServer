using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class TechMap
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Version { get; set; }

    public int StatusId { get; set; }

    public int AuthorId { get; set; }

    public DateOnly? CreationDate { get; set; }

    public DateOnly? ApprovalDate { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public virtual User Author { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();

    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TechMapStep> TechMapSteps { get; set; } = new List<TechMapStep>();
}
