using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class Product
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Type { get; set; }

    public string? ReleaseForm { get; set; }

    public int StatusId { get; set; }

    public int? ActiveRecipeId { get; set; }

    public int? ActiveTechMapId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
    public string? Comment { get; set; }
    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();
    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<TechMap> TechMaps { get; set; } = new List<TechMap>();
}
