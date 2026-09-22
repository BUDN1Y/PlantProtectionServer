namespace PlantProtectionServer.Models.MachineOperatorMobile
{
    public class ProductionBatch
    {
        public int id { get; set; }
        public string batchNumber { get; set; } = null!;
        public int orderId { get; set; }
        public int productId { get; set; }
        public int recipeVersionId { get; set; }
        public int techMapVersionId { get; set; }
        public string? startDate { get; set; }
        public string? endDate { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; } = null!;
        public int? currentStepId { get; set; }
        public int? extruderProgramId { get; set; }
        public string? createdAt { get; set; }
        public string? updatedAt { get; set; }
        public string? labDecision { get; set; }
        public string? labComment { get; set; }
    }
}
