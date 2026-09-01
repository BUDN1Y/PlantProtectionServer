namespace PlantProtectionServer.Models
{
    public class ProductDto
    {
        public int id { get; set; }
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? type { get; set; }
        public string? releaseForm { get; set; }
        public int statusId { get; set; }
        public string statusName { get; set; } = null!;
        public string statusColor { get; set; } = null!;
        public string? activeRecipeFill { get; set; }      
        public string? activeTechMapFill { get; set; }
        public int? activeRecipeId { get; set; }
        public int? activeTechMapId { get; set; }
        public string? comment { get; set; }


    }
}
