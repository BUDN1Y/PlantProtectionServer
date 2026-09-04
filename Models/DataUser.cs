namespace PlantProtectionServer.Models
{
    public class DataUser
    {
        public int id { get; set; }
        public string fullName { get; set; } = null!;
        public string? roleName { get; set; } = null!;
        public int roleId { get; set; }
        public string? departmentName { get; set; } = null!;
        public string? departmentDescription { get; set; }
        public string? roleDescription { get; set; }
        public bool? isActive { get; set; }
    }
}
