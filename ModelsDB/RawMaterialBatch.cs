using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class RawMaterialBatch
{
    public int Id { get; set; }

    public string BatchNumber { get; set; } = null!;

    public int RawMaterialId { get; set; }

    public string? Supplier { get; set; }

    public DateOnly ReceiptDate { get; set; }

    public decimal Quantity { get; set; }

    public string Unit { get; set; } = null!;

    public string? StorageLocation { get; set; }

    public int StatusId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? LastTestId { get; set; }

    public virtual ICollection<BatchUsedMaterial> BatchUsedMaterials { get; set; } = new List<BatchUsedMaterial>();

    public virtual RawMaterial RawMaterial { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
