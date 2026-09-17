namespace PlantProtectionServer.Models.MachineOperator
{
    public class TechMapStep
    {
        public int id { get; set; }
        public int stepNumber { get; set; }
        public string name { get; set; } = null!;
        public string stepType { get; set; } = null!;
        public string? instruction { get; set; }
        public decimal? plannedTemp { get; set; }
        public decimal? plannedPressure { get; set; }
        public int? plannedTimeMin { get; set; }
        public int? plannedTimeMax { get; set; }
        public decimal? toleranceTempMin { get; set; }
        public decimal? toleranceTempMax { get; set; }
        public decimal? tolerancePressureMin { get; set; }
        public decimal? tolerancePressureMax { get; set; }
        public bool? isMandatory { get; set; }
    }
}
