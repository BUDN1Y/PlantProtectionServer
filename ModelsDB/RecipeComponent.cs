using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class RecipeComponent
{
    public int Id { get; set; }

    public int RecipeId { get; set; }

    public int RawMaterialId { get; set; }

    public decimal Percentage { get; set; }

    public decimal? ToleranceMin { get; set; }

    public decimal? ToleranceMax { get; set; }

    public int LoadOrder { get; set; }

    public virtual RawMaterial RawMaterial { get; set; } = null!;

    public virtual Recipe Recipe { get; set; } = null!;
}
