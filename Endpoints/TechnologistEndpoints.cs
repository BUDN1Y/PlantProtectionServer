using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models.Technologist.Product;
using PlantProtectionServer.Models.Technologist.Recipe;

namespace PlantProtectionServer.Endpoints
{
    public static class TechnologistEndpoints
    {
        public static IEndpointRouteBuilder MapTechnologistEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/appTechnologi");

            group.MapGet("/authorization", async (string log, string pass) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                DataUser? authorization = await appTechnologi.Authorization(log, pass);            
                return authorization;
            });

            group.MapGet("/getDataProduction", async () =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var result = await appTechnologi.GetDataProduction();

                if (result != null)
                {
                    return Results.Ok(result);
                }
                else
                {
                    return Results.NotFound("Данные не найдены");
                }
            });

            group.MapPost("/addNewProduct", async (context) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var request = await context.Request.ReadFromJsonAsync<ConfirmationProduct>();
                bool result = await appTechnologi.AddNewProduct(request);

                if (result)
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            group.MapPut("/editProduct", async (context) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var product = await context.Request.ReadFromJsonAsync<ConfirmationProduct>();
                bool edit = await appTechnologi.EditProduct(product);
                if (edit)
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            group.MapPut("/changetStatusProduct", async (context) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var product = await context.Request.ReadFromJsonAsync<ConfirmationProduct>();
                bool edit = await appTechnologi.EditStatusProduct(product);

                if (edit)
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            group.MapGet("/getDataRecipes", async () =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                return await appTechnologi.AllRecipesData();
            });

            group.MapGet("/getDataRecipeComponets", async (int id) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                if (id == 0)
                {
                    return await appTechnologi.RecipeComponets(id);
                }
                else
                {
                    return await appTechnologi.RecipeComponets(id);
                }
            });

            group.MapGet("/getDataRawMaterials", async () =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                return await appTechnologi.GetDataRawMaterials();
            });

            group.MapGet("/getDataRecipesComment", async (int id, string type) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var result = await appTechnologi.GetRecipesComment(id, type);
                if (result == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(result);
                }
            });

            group.MapPost("/createRecipe", async (context) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var request = await context.Request.ReadFromJsonAsync<CreateRecipe>();
                bool result = await appTechnologi.AddNewRecipe(request);

                if (result)
                {
                    context.Response.StatusCode = 200;
                }
                else
                {
                    context.Response.StatusCode = 400;
                }
            });

            group.MapPut("/editRecipe", async (int id, HttpContext context) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var product = await context.Request.ReadFromJsonAsync<CreateRecipe>();
                bool edit = await appTechnologi.EditRecipe(id, product);

                if (edit == false)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(edit);
                }
            });

            group.MapPut("/editStatusRecipe", async (int id, int statusId, int userId) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                bool edit = await appTechnologi.EditStatusRecipe(id, statusId, userId);

                if (edit == false)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(edit);
                }
            });

            return app;
        }
    }
}
