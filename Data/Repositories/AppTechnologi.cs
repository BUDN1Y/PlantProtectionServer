using Microsoft.EntityFrameworkCore;
using PlantProtectionServer.Data.Context;
using PlantProtectionServer.Models;
using PlantProtectionServer.ModelsDB;
using System.Linq;

namespace PlantProtectionServer.Data.Repositories
{
    public class AppTechnologi
    {
        PlantProtectionDbContext context = new PlantProtectionDbContext();

        public async Task<bool> Authorization(string log, string pass)
        {
            return await context.Users.AnyAsync(x => x.Login == log && x.PasswordHash == pass);
        }

        public async Task<ProductDto[]> GetDataProduction()
        {
            return await context.Products
           .Include(p => p.Status)
           .Include(p => p.Recipes)
           .Include(p => p.TechMaps)
           .Select(p => new ProductDto
           {
               id = p.Id,
               code = p.Code,
               name = p.Name,
               type = p.Type,
               releaseForm = p.ReleaseForm,
               statusId = p.StatusId,
               statusName = p.Status.Name,
               statusColor = p.Status.Color ?? "#999999",
               activeRecipeFill = $"{p.ActiveRecipeId.ToString()} v{p.Recipes
                             .Where(r => r.Id == p.ActiveRecipeId)
                             .Select(r => r.Version)
                             .FirstOrDefault()}".Trim(),

               activeTechMapFill = $"{p.ActiveRecipeId.ToString()} v{p.TechMaps
                             .Where(t => t.Id == p.ActiveTechMapId)
                             .Select(t => t.Version.ToString())
                             .FirstOrDefault()}".Trim(),

               activeRecipeId = p.ActiveRecipeId,
               activeTechMapId = p.ActiveTechMapId,
               comment = p.Comment
           }).ToArrayAsync();
        }

        public async Task<bool> AddNewProduct(ConfirmationProduct product)
        {
            try
            {
                await context.Products.AddAsync(new Product()
                {
                    Name = product.name,
                    Code = product.code,
                    Type = product.type,
                    ReleaseForm = product.releaseForm,
                    Comment = product.comment,
                    StatusId = product.status,
                    CreatedAt = DateTime.Now
                });
                await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex);
                return false;
            }
        }

    }
}
