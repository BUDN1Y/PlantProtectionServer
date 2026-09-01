using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class LabTest
{
    public int Id { get; set; }

    public string TestNumber { get; set; } = null!;

    public string TargetType { get; set; } = null!;

    public int TargetId { get; set; }

    public string TestType { get; set; } = null!;

    public DateTime? AssignedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public int? AssignedTo { get; set; }

    public int? PerformedBy { get; set; }

    public int StatusId { get; set; }

    public string? Priority { get; set; }

    public string? Comment { get; set; }

    public string? Decision { get; set; }

    public string? DecisionReason { get; set; }

    public DateTime? DecidedAt { get; set; }

    public int? DecidedBy { get; set; }

    public virtual User? AssignedToNavigation { get; set; }

    public virtual User? DecidedByNavigation { get; set; }

    public virtual ICollection<LabTestParameter> LabTestParameters { get; set; } = new List<LabTestParameter>();

    public virtual User? PerformedByNavigation { get; set; }

    public virtual Status Status { get; set; } = null!;
}
