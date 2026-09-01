using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class RawMaterial
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Category { get; set; }

    public string Unit { get; set; } = null!;

    public decimal? StandardPrice { get; set; }

    public virtual ICollection<RawMaterialBatch> RawMaterialBatches { get; set; } = new List<RawMaterialBatch>();

    public virtual ICollection<RecipeComponent> RecipeComponents { get; set; } = new List<RecipeComponent>();
}
