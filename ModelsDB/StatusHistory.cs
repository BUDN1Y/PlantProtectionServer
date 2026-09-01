using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class StatusHistory
{
    public int Id { get; set; }

    public string EntityType { get; set; } = null!;

    public int EntityId { get; set; }

    public int? OldStatusId { get; set; }

    public int NewStatusId { get; set; }

    public int ChangedBy { get; set; }

    public DateTime? ChangedAt { get; set; }

    public string? Comment { get; set; }

    public virtual User ChangedByNavigation { get; set; } = null!;

    public virtual Status NewStatus { get; set; } = null!;

    public virtual Status? OldStatus { get; set; }
}
