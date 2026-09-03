using Azure.Core;
using Microsoft.AspNetCore.Http;
using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models;
using PlantProtectionServer.ModelsDB;
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
                bool authorization = await appTechnologi.Authorization(log, pass);
                return authorization;
                //if (authorization)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}

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

                if(result)
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
           

            app.Run();


        }
    }
}
