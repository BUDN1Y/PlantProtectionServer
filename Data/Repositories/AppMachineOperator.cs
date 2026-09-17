using Microsoft.EntityFrameworkCore;
using PlantProtectionServer.Data.Context;
using PlantProtectionServer.Models.MachineOperator;
using PlantProtectionServer.ModelsDB;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlantProtectionServer.Data.Repositories
{
    public class AppMachineOperator
    {
        PlantProtectionDbContext context = new PlantProtectionDbContext();

        public async Task<UserData?> Authorization(string log, string pass)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Login == log && x.PasswordHash == pass);

                if (user != null)
                {
                    return new UserData()
                    {
                        id = user.Id,
                        fio = user.FullName,
                        roleId = user.RoleId,
                    };
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<List<ActualBetch>?> ActualBetchTable()
        {
            try
            {
                var dataBetch = await context.ProductionBatches
                                .Include(x => x.Product)
                                .Include(x => x.Status)
                                .Include(x => x.Order)
                                .Include(x => x.BatchStepExecutions)
                                .ThenInclude(bse => bse.Status)
                                .Include(x => x.BatchStepExecutions)
                                .ThenInclude(bse => bse.TechStep)
                                .Include(x => x.Deviations)
                                .Where(x => x.Status.Code == "batch_in_progress"
                                      || x.Status.Code == "batch_started"
                                      || x.Status.Code == "batch_paused")
                                .Select(x => new ActualBetch()
                                {
                                    id = x.Id,
                                    techMapId = x.TechMapVersionId,
                                    batchNumber = x.BatchNumber,
                                    statusName = x.Status.Name,
                                    statusColor = (x.Status.Color == null) ? "#999" : x.Status.Color,
                                    productName = x.Product.Name,


                                    currentStepName = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.TechStep.Name)
                                         .FirstOrDefault(),


                                    currentStepStatusName = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.Status.Name)
                                         .FirstOrDefault(),


                                    currentStepStatusColor = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.Status.Color)
                                         .FirstOrDefault(),

                                    hasWarnings = x.Deviations.Any(d => d.Severity == "warning"
                                                                      && d.Status.Code != "dev_closed"),


                                    hasCriticals = x.Deviations.Any(d => d.Severity == "critical"
                                                                       && d.Status.Code != "dev_closed"),


                                    startDate = x.StartDate,


                                })
                                 .OrderBy(x => x.batchNumber)
                                 .ToListAsync();

                return dataBetch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<ProgramBatch?> GetProgramBatch(int batchId, int techMapId)
        {
            try
            {
                var batchStepExecutions = await context.BatchStepExecutions.Where(x => x.BatchId == batchId).ToListAsync();
                var techMapStep = await context.TechMapSteps.ToListAsync();
                var productionBatch = await context.ProductionBatches.FirstOrDefaultAsync(x => x.Id == batchId);
                var statusName = await context.Statuses.ToListAsync();

                var batchStepExecutionsProcessed = batchStepExecutions
    .Select(x => new Models.MachineOperator.BatchStepExecution
    {
        id = x.Id,
        statusName = statusName.FirstOrDefault(y => y.Id == x.StatusId)?.Name ?? "",
        batchId = x.BatchId,
        techStepId = x.TechStepId,
        startedAt = x.StartedAt,
        finishedAt = x.FinishedAt,
        actualTemp = x.ActualTemp,
        actualPressure = x.ActualPressure,
        actualTime = x.ActualTime,
        comment = x.Comment
    })
    .ToList();

                var techMapStepProcessed = techMapStep
    .Select(x => new Models.MachineOperator.TechMapStep
    {
        id = x.Id,
        stepNumber = x.StepNumber,
        name = x.Name,
        stepType = x.StepType,
        instruction = x.Instruction,
        plannedTemp = x.PlannedTemp,
        plannedPressure = x.PlannedPressure,
        plannedTimeMin = x.PlannedTimeMin,
        plannedTimeMax = x.PlannedTimeMax,
        toleranceTempMin = x.ToleranceTempMin,
        toleranceTempMax = x.ToleranceTempMax,
        tolerancePressureMin = x.TolerancePressureMin,
        tolerancePressureMax = x.TolerancePressureMax,
        isMandatory = x.IsMandatory
    })
    .ToList();

                ProgramBatch programBatch = new ProgramBatch()
                {
                    batchStepExecutions = batchStepExecutionsProcessed.ToList(),
                    techMapSteps = techMapStepProcessed.ToList()
                };

                Console.WriteLine(JsonSerializer.Serialize(programBatch));
                return programBatch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async void PutEndBatch(int batchId, int techMapStep, decimal? actualTemp, int? actualTime, decimal? actualPressure)
        {
            try
            {
                var batchStepExecution = await context.BatchStepExecutions.FirstAsync(x => x.BatchId == batchId && x.TechStepId == techMapStep);
                batchStepExecution.StatusId = 33;
                batchStepExecution.FinishedAt = DateTime.Now;
                batchStepExecution.FinishedBy = 2;
                batchStepExecution.ActualTemp = actualTemp;
                batchStepExecution.ActualTime = actualTime;

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }

        public async Task PostStartBatch(int batchId, int techMapStep, string comment)
        {
            try
            {
                var newBatchStepExecution = new ModelsDB.BatchStepExecution()
                {
                    BatchId = batchId,
                    TechStepId = techMapStep,
                    StatusId = 32,
                    StartedAt = DateTime.Now,
                    StartedBy = 2,
                    Comment = comment
                };

                context.BatchStepExecutions.Add(newBatchStepExecution);

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
        }
    }
}
