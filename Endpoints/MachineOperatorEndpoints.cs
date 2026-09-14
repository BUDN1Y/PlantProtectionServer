namespace PlantProtectionServer.Endpoints
{
    public static class MachineOperatorEndpoints
    {
        public static IEndpointRouteBuilder MapMachineOperatorEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appMachineOperator");


            return app;
        }
    }
}
