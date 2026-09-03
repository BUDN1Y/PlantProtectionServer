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

        Dictionary<int, string> comment = new Dictionary<int, string>() 
        {
            {3, "Заархивирование продукта"},
            {2, "Восстановление продукта" },
            {9, "Подтверждение продукта" }
        };
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

        public async Task<bool> EditProduct(ConfirmationProduct editProduct)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Code == editProduct.oldCode);
                product.Code = editProduct.code;
                product.Name = editProduct.name;
                product.Type = editProduct.type;
                product.ReleaseForm = editProduct.releaseForm;
                product.Comment = editProduct.comment;
                product.UpdatedAt = DateTime.Now;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        
        public async Task<bool> EditStatusProduct(ConfirmationProduct editProduct)
        {
            try
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Code == editProduct.code);
                product.StatusId = editProduct.status;
                product.UpdatedAt = DateTime.Now;

                Console.WriteLine($"{product.ActiveRecipeId} {product.ActiveTechMapId} {editProduct.status}");
                if((product.ActiveRecipeId == null || product.ActiveTechMapId == null) && editProduct.status == 2)
                {
                    editProduct.status = 9;
                    product.StatusId = editProduct.status;
                }

                await context.StatusHistories.AddAsync(new StatusHistory()
                {
                    EntityType = "product",
                    EntityId = Convert.ToInt32(editProduct.id), //id кого продукта изменился
                    NewStatusId = editProduct.status,
                    OldStatusId = editProduct.oldStatus,
                    ChangedAt = DateTime.Now,
                    Comment = comment[editProduct.status],
                    ChangedBy = 1, //кто измени id пользователя
                    

                });

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }

        
    }
}
