using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models.Technologist.Product;

namespace PlantProtectionServer.Endpoints
{
    public static class MachineOperatorMobileEndpoints
    {

        public static IEndpointRouteBuilder MapTechnologistMobileEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appMachineOperatorMobile");

            group.MapGet("/authorization", async (string log, string pass) =>
            {
                Console.WriteLine("Успешно");
                AppMachineOperatorMobile appTechnologi = new AppMachineOperatorMobile();
                DataUser? authorization = await appTechnologi.Authorization(log, pass);
                return authorization;
            });

            return group;
        }
    }
}
