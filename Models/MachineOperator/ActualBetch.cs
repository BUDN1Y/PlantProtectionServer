namespace PlantProtectionServer.Models.MachineOperator
{
    public class ActualBetch
    {
        public int id { get; set; }
        public int techMapId { get; set; }
        public string batchNumber { get; set; } = null!;
        public string statusName { get; set; } = null!;
        public string statusColor { get; set; } = null!;
        public string productName { get; set; } = null!;
        public string? currentStepName { get; set; }
        public string? currentStepStatusName { get; set; } 
        public string? currentStepStatusColor { get; set; }
        public bool hasWarnings { get; set; }
        public bool hasCriticals { get; set; }
        public DateOnly? startDate { get; set; }
    }
}
