using Azure.Core;
using Microsoft.AspNetCore.Http;
using PlantProtectionServer.Data.Repositories;
using PlantProtectionServer.Models.Technologist.Product;
using PlantProtectionServer.ModelsDB;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks.Dataflow;
using PlantProtectionServer.Models.Technologist.Recipe;
using PlantProtectionServer.Endpoints;


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

            app.UseStaticFiles();

            app.MapTechnologistEndpoints();
            app.MapMachineOperatorEndpoints();

            app.MapGet("/", async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("wwwroot/Html/index.html");
            });

            app.Run();


        }
}
}
