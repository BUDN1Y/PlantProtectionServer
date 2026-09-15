using Microsoft.EntityFrameworkCore;
using PlantProtectionServer.Data.Context;
using PlantProtectionServer.Models.MachineOperator;
using PlantProtectionServer.ModelsDB;
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
                                    batchNumber = x.BatchNumber,
                                    statusName = x.Status.Name,
                                    statusColor = (x.Status.Color == null) ? "#999" : x.Status.Color,
                                    productName = x.Product.Name,

                                    // Текущий шаг: ищем активный BatchStepExecution (статус "step_in_progress")
                                    currentStepName = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.TechStep.Name)
                                         .FirstOrDefault(),

                                    // Статус текущего шага
                                    currentStepStatusName = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.Status.Name)
                                         .FirstOrDefault(),

                                    // Цвет статуса шага
                                    currentStepStatusColor = x.BatchStepExecutions
                                         .Where(bse => bse.Status.Code == "step_in_progress")
                                         .Select(bse => bse.Status.Color)
                                         .FirstOrDefault(),

                                    // Есть ли предупреждения (severity = warning, статус не закрыт)
                                    hasWarnings = x.Deviations.Any(d => d.Severity == "warning"
                                                                      && d.Status.Code != "dev_closed"),

                                    // Есть ли критические отклонения
                                    hasCriticals = x.Deviations.Any(d => d.Severity == "critical"
                                                                       && d.Status.Code != "dev_closed"),

                                    // Время начала партии
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
    }
}
