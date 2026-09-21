using Microsoft.EntityFrameworkCore;
using PlantProtectionServer.Data.Context;
using PlantProtectionServer.Models.Technologist.Product;

namespace PlantProtectionServer.Data.Repositories
{
    public class AppMachineOperatorMobile
    {
        PlantProtectionDbContext context = new PlantProtectionDbContext();

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
    }
}
