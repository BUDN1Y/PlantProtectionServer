using System;
using System.Collections.Generic;

namespace PlantProtectionServer.ModelsDB;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public int RoleId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Phone { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<BatchStepExecution> BatchStepExecutionFinishedByNavigations { get; set; } = new List<BatchStepExecution>();

    public virtual ICollection<BatchStepExecution> BatchStepExecutionStartedByNavigations { get; set; } = new List<BatchStepExecution>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<Deviation> Deviations { get; set; } = new List<Deviation>();

    public virtual ICollection<LabTest> LabTestAssignedToNavigations { get; set; } = new List<LabTest>();

    public virtual ICollection<LabTest> LabTestDecidedByNavigations { get; set; } = new List<LabTest>();

    public virtual ICollection<LabTest> LabTestPerformedByNavigations { get; set; } = new List<LabTest>();

    public virtual ICollection<ProductionOrder> ProductionOrders { get; set; } = new List<ProductionOrder>();

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<StatusHistory> StatusHistories { get; set; } = new List<StatusHistory>();

    public virtual ICollection<TechMap> TechMaps { get; set; } = new List<TechMap>();
}
