using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PlantProtectionServer.ModelsDB;

namespace PlantProtectionServer.Data.Context;

public partial class PlantProtectionDbContext : DbContext
{
    public PlantProtectionDbContext()
    {
    }

    public PlantProtectionDbContext(DbContextOptions<PlantProtectionDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<BatchStepExecution> BatchStepExecutions { get; set; }

    public virtual DbSet<BatchUsedMaterial> BatchUsedMaterials { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Deviation> Deviations { get; set; }

    public virtual DbSet<ExtruderEvent> ExtruderEvents { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<LabTestParameter> LabTestParameters { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductionBatch> ProductionBatches { get; set; }

    public virtual DbSet<ProductionOrder> ProductionOrders { get; set; }

    public virtual DbSet<RawMaterial> RawMaterials { get; set; }

    public virtual DbSet<RawMaterialBatch> RawMaterialBatches { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<RecipeComponent> RecipeComponents { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<StatusHistory> StatusHistories { get; set; }

    public virtual DbSet<TechMap> TechMaps { get; set; }

    public virtual DbSet<TechMapStep> TechMapSteps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(@"Server=DESKTOP-8OLGP4Q\SQLEXPRESS;Database=PlantProtectionDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__audit_lo__3213E83F0F9D5AF3");

            entity.ToTable("audit_log");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "idx_audit_log_entity");

            entity.HasIndex(e => e.UserId, "idx_audit_log_user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("action_time");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .HasColumnName("action_type");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(50)
                .HasColumnName("entity_type");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.NewData).HasColumnName("new_data");
            entity.Property(e => e.OldData).HasColumnName("old_data");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.AuditLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_audit_log_users");
        });

        modelBuilder.Entity<BatchStepExecution>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__batch_st__3213E83FF69AB25D");

            entity.ToTable("batch_step_executions");

            entity.HasIndex(e => new { e.BatchId, e.TechStepId }, "UQ_batch_step_executions").IsUnique();

            entity.HasIndex(e => e.BatchId, "idx_batch_step_executions_batch_id");

            entity.HasIndex(e => e.StatusId, "idx_batch_step_executions_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualPressure)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("actual_pressure");
            entity.Property(e => e.ActualTemp)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("actual_temp");
            entity.Property(e => e.ActualTime).HasColumnName("actual_time");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.FinishedAt)
                .HasColumnType("datetime")
                .HasColumnName("finished_at");
            entity.Property(e => e.FinishedBy).HasColumnName("finished_by");
            entity.Property(e => e.StartedAt)
                .HasColumnType("datetime")
                .HasColumnName("started_at");
            entity.Property(e => e.StartedBy).HasColumnName("started_by");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TechStepId).HasColumnName("tech_step_id");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchStepExecutions)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_batch_step_executions_batch");

            entity.HasOne(d => d.FinishedByNavigation).WithMany(p => p.BatchStepExecutionFinishedByNavigations)
                .HasForeignKey(d => d.FinishedBy)
                .HasConstraintName("FK_batch_step_executions_finished_by");

            entity.HasOne(d => d.StartedByNavigation).WithMany(p => p.BatchStepExecutionStartedByNavigations)
                .HasForeignKey(d => d.StartedBy)
                .HasConstraintName("FK_batch_step_executions_started_by");

            entity.HasOne(d => d.Status).WithMany(p => p.BatchStepExecutions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_batch_step_executions_status");

            entity.HasOne(d => d.TechStep).WithMany(p => p.BatchStepExecutions)
                .HasForeignKey(d => d.TechStepId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_batch_step_executions_tech_step");
        });

        modelBuilder.Entity<BatchUsedMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__batch_us__3213E83F527366A5");

            entity.ToTable("batch_used_materials");

            entity.HasIndex(e => new { e.BatchId, e.RawMaterialBatchId }, "UQ_batch_used_materials").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.QuantityUsed)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("quantity_used");
            entity.Property(e => e.RawMaterialBatchId).HasColumnName("raw_material_batch_id");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");

            entity.HasOne(d => d.Batch).WithMany(p => p.BatchUsedMaterials)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_batch_used_materials_batch");

            entity.HasOne(d => d.RawMaterialBatch).WithMany(p => p.BatchUsedMaterials)
                .HasForeignKey(d => d.RawMaterialBatchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_batch_used_materials_raw_batch");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__departme__3213E83F79AE70E8");

            entity.ToTable("departments");

            entity.HasIndex(e => e.Name, "UQ__departme__72E12F1B63E6F294").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Deviation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__deviatio__3213E83F9D556A21");

            entity.ToTable("deviations");

            entity.HasIndex(e => e.BatchId, "idx_deviations_batch_id");

            entity.HasIndex(e => e.Severity, "idx_deviations_severity");

            entity.HasIndex(e => e.StatusId, "idx_deviations_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualValue)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("actual_value");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.ParameterName)
                .HasMaxLength(100)
                .HasColumnName("parameter_name");
            entity.Property(e => e.PlannedValue)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("planned_value");
            entity.Property(e => e.ReportedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("reported_at");
            entity.Property(e => e.ReportedBy).HasColumnName("reported_by");
            entity.Property(e => e.Severity)
                .HasMaxLength(20)
                .HasDefaultValue("warning")
                .HasColumnName("severity");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StepExecutionId).HasColumnName("step_execution_id");

            entity.HasOne(d => d.Batch).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_deviations_batch");

            entity.HasOne(d => d.ReportedByNavigation).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.ReportedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_deviations_reported_by");

            entity.HasOne(d => d.Status).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_deviations_status");

            entity.HasOne(d => d.StepExecution).WithMany(p => p.Deviations)
                .HasForeignKey(d => d.StepExecutionId)
                .HasConstraintName("FK_deviations_step_execution");
        });

        modelBuilder.Entity<ExtruderEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__extruder__3213E83F078AC35F");

            entity.ToTable("extruder_events");

            entity.HasIndex(e => e.BatchId, "idx_extruder_events_batch_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.EventTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("event_time");
            entity.Property(e => e.ParameterName)
                .HasMaxLength(50)
                .HasColumnName("parameter_name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("normal")
                .HasColumnName("status");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("value");
            entity.Property(e => e.Zone)
                .HasMaxLength(50)
                .HasColumnName("zone");

            entity.HasOne(d => d.Batch).WithMany(p => p.ExtruderEvents)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_extruder_events_batch");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lab_test__3213E83FF9A90AFB");

            entity.ToTable("lab_tests");

            entity.HasIndex(e => e.TestNumber, "UQ__lab_test__D7F6F6933C9A4886").IsUnique();

            entity.HasIndex(e => e.StatusId, "idx_lab_tests_status");

            entity.HasIndex(e => new { e.TargetType, e.TargetId }, "idx_lab_tests_target");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("assigned_at");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CompletedAt)
                .HasColumnType("datetime")
                .HasColumnName("completed_at");
            entity.Property(e => e.DecidedAt)
                .HasColumnType("datetime")
                .HasColumnName("decided_at");
            entity.Property(e => e.DecidedBy).HasColumnName("decided_by");
            entity.Property(e => e.Decision)
                .HasMaxLength(20)
                .HasColumnName("decision");
            entity.Property(e => e.DecisionReason).HasColumnName("decision_reason");
            entity.Property(e => e.PerformedBy).HasColumnName("performed_by");
            entity.Property(e => e.Priority)
                .HasMaxLength(20)
                .HasDefaultValue("normal")
                .HasColumnName("priority");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TargetId).HasColumnName("target_id");
            entity.Property(e => e.TargetType)
                .HasMaxLength(20)
                .HasColumnName("target_type");
            entity.Property(e => e.TestNumber)
                .HasMaxLength(50)
                .HasColumnName("test_number");
            entity.Property(e => e.TestType)
                .HasMaxLength(50)
                .HasColumnName("test_type");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.LabTestAssignedToNavigations)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK_lab_tests_assigned_to");

            entity.HasOne(d => d.DecidedByNavigation).WithMany(p => p.LabTestDecidedByNavigations)
                .HasForeignKey(d => d.DecidedBy)
                .HasConstraintName("FK_lab_tests_decided_by");

            entity.HasOne(d => d.PerformedByNavigation).WithMany(p => p.LabTestPerformedByNavigations)
                .HasForeignKey(d => d.PerformedBy)
                .HasConstraintName("FK_lab_tests_performed_by");

            entity.HasOne(d => d.Status).WithMany(p => p.LabTests)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_lab_tests_status");
        });

        modelBuilder.Entity<LabTestParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__lab_test__3213E83F25FFC5F2");

            entity.ToTable("lab_test_parameters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualValue)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("actual_value");
            entity.Property(e => e.LabTestId).HasColumnName("lab_test_id");
            entity.Property(e => e.NormMax)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("norm_max");
            entity.Property(e => e.NormMin)
                .HasColumnType("decimal(12, 4)")
                .HasColumnName("norm_min");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.ParameterName)
                .HasMaxLength(100)
                .HasColumnName("parameter_name");
            entity.Property(e => e.ResultStatus)
                .HasMaxLength(20)
                .HasColumnName("result_status");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestParameters)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK_lab_test_parameters_lab_test");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FD7C2A4C3");

            entity.ToTable("products");

            entity.HasIndex(e => e.Code, "UQ__products__357D4CF9D86D6434").IsUnique();

            entity.HasIndex(e => e.StatusId, "idx_products_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActiveRecipeId).HasColumnName("active_recipe_id");
            entity.Property(e => e.ActiveTechMapId).HasColumnName("active_tech_map_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.ReleaseForm)
                .HasMaxLength(50)
                .HasColumnName("release_form");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_products_status");
        });

        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producti__3213E83F40D3F453");

            entity.ToTable("production_batches");

            entity.HasIndex(e => e.BatchNumber, "UQ__producti__56E3783750B22D41").IsUnique();

            entity.HasIndex(e => e.OrderId, "idx_production_batches_order_id");

            entity.HasIndex(e => e.StatusId, "idx_production_batches_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(50)
                .HasColumnName("batch_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentStepId).HasColumnName("current_step_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ExtruderProgramId).HasColumnName("extruder_program_id");
            entity.Property(e => e.LabComment).HasColumnName("lab_comment");
            entity.Property(e => e.LabDecision)
                .HasMaxLength(20)
                .HasColumnName("lab_decision");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RecipeVersionId).HasColumnName("recipe_version_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TechMapVersionId).HasColumnName("tech_map_version_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.CurrentStep).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.CurrentStepId)
                .HasConstraintName("FK_production_batches_tech_map_steps");

            entity.HasOne(d => d.Order).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_batches_orders");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_batches_products");

            entity.HasOne(d => d.RecipeVersion).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.RecipeVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_batches_recipes");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_batches_status");

            entity.HasOne(d => d.TechMapVersion).WithMany(p => p.ProductionBatches)
                .HasForeignKey(d => d.TechMapVersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_batches_tech_maps");
        });

        modelBuilder.Entity<ProductionOrder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producti__3213E83F19A7A77E");

            entity.ToTable("production_orders");

            entity.HasIndex(e => e.OrderNumber, "UQ__producti__730E34DF2C87A643").IsUnique();

            entity.HasIndex(e => e.StatusId, "idx_production_orders_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .HasColumnName("order_number");
            entity.Property(e => e.PlannedEndDate).HasColumnName("planned_end_date");
            entity.Property(e => e.PlannedQuantity)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("planned_quantity");
            entity.Property(e => e.PlannedStartDate).HasColumnName("planned_start_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TechMapId).HasColumnName("tech_map_id");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_orders_users");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_orders_products");

            entity.HasOne(d => d.Recipe).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_orders_recipes");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_orders_status");

            entity.HasOne(d => d.TechMap).WithMany(p => p.ProductionOrders)
                .HasForeignKey(d => d.TechMapId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_production_orders_tech_maps");
        });

        modelBuilder.Entity<RawMaterial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__raw_mate__3213E83F860616CB");

            entity.ToTable("raw_materials");

            entity.HasIndex(e => e.Code, "UQ__raw_mate__357D4CF9962D75B1").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .HasColumnName("category");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.StandardPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("standard_price");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
        });

        modelBuilder.Entity<RawMaterialBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__raw_mate__3213E83F1D8C11E7");

            entity.ToTable("raw_material_batches");

            entity.HasIndex(e => e.BatchNumber, "UQ__raw_mate__56E378377CC8872D").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(50)
                .HasColumnName("batch_number");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.LastTestId).HasColumnName("last_test_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("quantity");
            entity.Property(e => e.RawMaterialId).HasColumnName("raw_material_id");
            entity.Property(e => e.ReceiptDate).HasColumnName("receipt_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StorageLocation)
                .HasMaxLength(50)
                .HasColumnName("storage_location");
            entity.Property(e => e.Supplier)
                .HasMaxLength(100)
                .HasColumnName("supplier");
            entity.Property(e => e.Unit)
                .HasMaxLength(20)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.RawMaterial).WithMany(p => p.RawMaterialBatches)
                .HasForeignKey(d => d.RawMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_raw_material_batches_raw_materials");

            entity.HasOne(d => d.Status).WithMany(p => p.RawMaterialBatches)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_raw_material_batches_status");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipes__3213E83FD8DB3A42");

            entity.ToTable("recipes");

            entity.HasIndex(e => new { e.ProductId, e.Version }, "UQ_recipes_product_version").IsUnique();

            entity.HasIndex(e => e.ProductId, "idx_recipes_product_id");

            entity.HasIndex(e => e.StatusId, "idx_recipes_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnName("creation_date");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TotalPercent)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("total_percent");
            entity.Property(e => e.Version)
                .HasDefaultValue(1)
                .HasColumnName("version");

            entity.HasOne(d => d.Author).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipes_users");

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipes_products");

            entity.HasOne(d => d.Status).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipes_status");
        });

        modelBuilder.Entity<RecipeComponent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__recipe_c__3213E83F6141D33E");

            entity.ToTable("recipe_components");

            entity.HasIndex(e => new { e.RecipeId, e.RawMaterialId }, "UQ_recipe_components_recipe_material").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LoadOrder).HasColumnName("load_order");
            entity.Property(e => e.Percentage)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("percentage");
            entity.Property(e => e.RawMaterialId).HasColumnName("raw_material_id");
            entity.Property(e => e.RecipeId).HasColumnName("recipe_id");
            entity.Property(e => e.ToleranceMax)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("tolerance_max");
            entity.Property(e => e.ToleranceMin)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("tolerance_min");

            entity.HasOne(d => d.RawMaterial).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.RawMaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_recipe_components_raw_materials");

            entity.HasOne(d => d.Recipe).WithMany(p => p.RecipeComponents)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_recipe_components_recipes");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F059988EE");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "UQ__roles__72E12F1B2F03A1AD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statuses__3213E83F9F684C9A");

            entity.ToTable("statuses");

            entity.HasIndex(e => e.Code, "UQ__statuses__357D4CF9E1D00012").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(30)
                .HasColumnName("code");
            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .HasColumnName("color");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.EntityType)
                .HasMaxLength(30)
                .HasColumnName("entity_type");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasColumnName("is_default");
            entity.Property(e => e.IsFinal)
                .HasDefaultValue(false)
                .HasColumnName("is_final");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.SortOrder)
                .HasDefaultValue(0)
                .HasColumnName("sort_order");
        });

        modelBuilder.Entity<StatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__status_h__3213E83FD7AC8DD3");

            entity.ToTable("status_history");

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "idx_status_history_entity");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("changed_at");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.EntityType)
                .HasMaxLength(30)
                .HasColumnName("entity_type");
            entity.Property(e => e.NewStatusId).HasColumnName("new_status_id");
            entity.Property(e => e.OldStatusId).HasColumnName("old_status_id");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.StatusHistories)
                .HasForeignKey(d => d.ChangedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_status_history_changed_by");

            entity.HasOne(d => d.NewStatus).WithMany(p => p.StatusHistoryNewStatuses)
                .HasForeignKey(d => d.NewStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_status_history_new_status");

            entity.HasOne(d => d.OldStatus).WithMany(p => p.StatusHistoryOldStatuses)
                .HasForeignKey(d => d.OldStatusId)
                .HasConstraintName("FK_status_history_old_status");
        });

        modelBuilder.Entity<TechMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tech_map__3213E83F66D7C596");

            entity.ToTable("tech_maps");

            entity.HasIndex(e => new { e.ProductId, e.Version }, "UQ_tech_maps_product_version").IsUnique();

            entity.HasIndex(e => e.ProductId, "idx_tech_maps_product_id");

            entity.HasIndex(e => e.StatusId, "idx_tech_maps_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(CONVERT([date],getdate()))")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(false)
                .HasColumnName("is_active");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Version)
                .HasDefaultValue(1)
                .HasColumnName("version");

            entity.HasOne(d => d.Author).WithMany(p => p.TechMaps)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tech_maps_users");

            entity.HasOne(d => d.Product).WithMany(p => p.TechMaps)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tech_maps_products");

            entity.HasOne(d => d.Status).WithMany(p => p.TechMaps)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tech_maps_status");
        });

        modelBuilder.Entity<TechMapStep>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tech_map__3213E83FBF5D7D2E");

            entity.ToTable("tech_map_steps");

            entity.HasIndex(e => new { e.TechMapId, e.StepNumber }, "UQ_tech_map_steps_map_number").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Instruction).HasColumnName("instruction");
            entity.Property(e => e.IsMandatory)
                .HasDefaultValue(true)
                .HasColumnName("is_mandatory");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PlannedPressure)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("planned_pressure");
            entity.Property(e => e.PlannedTemp)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("planned_temp");
            entity.Property(e => e.PlannedTimeMax).HasColumnName("planned_time_max");
            entity.Property(e => e.PlannedTimeMin).HasColumnName("planned_time_min");
            entity.Property(e => e.StepNumber).HasColumnName("step_number");
            entity.Property(e => e.StepType)
                .HasMaxLength(50)
                .HasColumnName("step_type");
            entity.Property(e => e.TechMapId).HasColumnName("tech_map_id");
            entity.Property(e => e.TolerancePressureMax)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("tolerance_pressure_max");
            entity.Property(e => e.TolerancePressureMin)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("tolerance_pressure_min");
            entity.Property(e => e.ToleranceTempMax)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("tolerance_temp_max");
            entity.Property(e => e.ToleranceTempMin)
                .HasColumnType("decimal(8, 2)")
                .HasColumnName("tolerance_temp_min");

            entity.HasOne(d => d.TechMap).WithMany(p => p.TechMapSteps)
                .HasForeignKey(d => d.TechMapId)
                .HasConstraintName("FK_tech_map_steps_tech_maps");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F51ECD1DB");

            entity.ToTable("users");

            entity.HasIndex(e => e.Login, "UQ__users__7838F27269904680").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .HasColumnName("full_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_users_departments");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_users_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
