namespace PlantProtectionServer.Models.MachineOperator
{
    public class ProgramBatch
    {
        public List<BatchStepExecution> batchStepExecutions { get; set; } = new List<BatchStepExecution>();
        public List<TechMapStep> techMapSteps { get; set; } = new List<TechMapStep>();
    }
}
