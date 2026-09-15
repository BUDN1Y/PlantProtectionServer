using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models.MachineOperator;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlantProtectionServer.Endpoints
{
    public static class MachineOperatorEndpoints
    {
        public static IEndpointRouteBuilder MapMachineOperatorEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appMachineOperator");

            group.MapGet("/authorization", async (string log, string pass, HttpContext context) =>
            {
                AppMachineOperator machineOperator = new AppMachineOperator();
                var user = await machineOperator.Authorization(log, pass);

                if (user != null)
                {
                    return Results.Ok(user);
                }
                else
                {
                    return Results.NotFound();
                }
            });

            group.MapGet("/actualBetchTable", async () =>
            {
                AppMachineOperator machineOperator = new AppMachineOperator();
                var actealBetch = await machineOperator.ActualBetchTable();
                if (actealBetch != null)
                {
                    return Results.Ok(actealBetch);
                }
                else
                {
                    return Results.NotFound();
                }

            });

            return app;
        }
    }
}
