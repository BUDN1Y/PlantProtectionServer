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
        public async Task<DataUser?> Authorization(string log, string pass)
        {
            return await context.Users
            .Include(p => p.Department)
            .Include(p => p.Role)
            .Where(x => x.Login == log && x.PasswordHash == pass)
            .Select(p => new DataUser
            {
                id = p.Id,
                fullName = p.FullName,
                roleName = p.Role.Name,
                roleId = p.RoleId,
                departmentName = p.Department.Name,
                departmentDescription = p.Department.Description,
                roleDescription = p.Role.Description,
                isActive = p.IsActive
            }).FirstOrDefaultAsync();
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
            catch (Exception ex)
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
                if ((product.ActiveRecipeId == null || product.ActiveTechMapId == null) && editProduct.status == 2)
                {
                    editProduct.status = 9;
                    product.StatusId = editProduct.status;
                }

                await context.StatusHistories.AddAsync(new StatusHistory()
                {
                    EntityType = "product",
                    EntityId = Convert.ToInt32(editProduct.id), //id какого продукта изменился
                    NewStatusId = editProduct.status,
                    OldStatusId = editProduct.oldStatus,
                    ChangedAt = DateTime.Now,
                    Comment = comment[editProduct.status],
                    ChangedBy = Convert.ToInt32(editProduct.changetBy), //кто измени id пользователя

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


        public async Task<RecipesData[]?> AllRecipesData()
        {
            try
            {
                return await context.Recipes
                .Include(p => p.Status)
                .Include(p => p.Author)
                    .Select(x => new RecipesData
                {
                    id = x.Id,
                    productId = x.ProductId,
                    creationDate = x.CreationDate,
                    version = x.Version,
                    comments = x.Comments,
                    statusId = x.StatusId,
                    authorId = x.AuthorId,
                    authorName = x.Author.FullName,
                    statusName = x.Status.Name,
                    statusColor = x.Status.Color ?? "#999999"
                    }).ToArrayAsync();
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<RecipeComponents[]?> RecipeComponets(int recipeId)
        {
            try
            {
                return await context.RecipeComponents.Where(x => x.RecipeId == recipeId).Select(x => new RecipeComponents
                {
                    id = x.Id,
                    recipeId = x.RecipeId,
                    rawMaterialId = x.RawMaterialId,
                    percentage = x.Percentage,
                    toleranceMin = x.ToleranceMin,
                    toleranceMax = x.ToleranceMax,
                    loadOrder = x.LoadOrder
                    
                }).ToArrayAsync();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
