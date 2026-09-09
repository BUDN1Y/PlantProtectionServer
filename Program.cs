using Azure.Core;
using Microsoft.AspNetCore.Http;
using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models;
using PlantProtectionServer.ModelsDB;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks.Dataflow;


namespace PlantProtectionServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();

            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapRazorPages()
               .WithStaticAssets();

            app.MapGet("/api/appTechnologi/authorization", async (string log, string pass) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                DataUser? authorization = await appTechnologi.Authorization(log, pass);
                return authorization;
            });

            app.MapGet("/api/appTechnologi/getDataProduction", async () =>
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

            app.MapPost("/api/appTechnologi/addNewProduct", async (context) =>
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

            app.MapPut("/api/appTechnologi/editProduct", async (context) =>
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

            app.MapPut("/api/appTechnologi/changetStatusProduct", async (context) =>
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

            app.MapGet("/api/appTechnologi/getDataRecipes", async () =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                return await appTechnologi.AllRecipesData();
            });

            app.MapGet("/api/appTechnologi/getDataRecipeComponets", async (int id) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                return await appTechnologi.RecipeComponets(id);
            });

            app.MapGet("/api/appTechnologi/getDataRawMaterials", async () =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                return await appTechnologi.GetDataRawMaterials();
            });

            app.MapGet("/api/appTechnologi/getDataRecipesComment", async (int id, string type) =>
            {
                AppTechnologi appTechnologi = new AppTechnologi();
                var result = await appTechnologi.GetRecipesComment(id, type);
                if(result == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(result);
                }
            });

            app.Run();


        }
    }
}
