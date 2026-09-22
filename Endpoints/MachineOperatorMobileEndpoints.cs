using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models.Technologist.Product;
using PlantProtectionServer.Models.MachineOperatorMobile;

namespace PlantProtectionServer.Endpoints
{
    public static class MachineOperatorMobileEndpoints
    {

        public static IEndpointRouteBuilder MapTechnologistMobileEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appMachineOperatorMobile");

            group.MapGet("/authorization", async (string log, string pass) =>
            {
                Console.WriteLine("authorization Успешно");
                AppMachineOperatorMobile appMachineOperatorMobile = new AppMachineOperatorMobile();
                DataUser? authorization = await appMachineOperatorMobile.Authorization(log, pass);
                return authorization;
            });


            group.MapGet("productionBatch", async () =>
            {
                Console.WriteLine("prodictionBatch Успешно");
                AppMachineOperatorMobile appMachineOperatorMobile = new AppMachineOperatorMobile();
                var productionBatch = await appMachineOperatorMobile.GetAllProductionBatch();
                return productionBatch;
            });
            return group;
        }
    }
}
