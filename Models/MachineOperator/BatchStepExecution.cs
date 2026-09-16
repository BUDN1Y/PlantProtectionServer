namespace PlantProtectionServer.Models.MachineOperator
{
    public class BatchStepExecution
    {
        public int id { get; set; }
        public string statusName { get; set; } = null!;
        public DateTime? startedAt { get; set; }
        public DateTime? finishedAt { get; set; }
        public decimal? actualTemp { get; set; }
        public decimal? actualPressure { get; set; }
        public int? actualTime { get; set; }
        public string? comment { get; set; }
    }
}
