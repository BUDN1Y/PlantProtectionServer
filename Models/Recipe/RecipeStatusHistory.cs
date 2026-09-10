namespace PlantProtectionServer.Models.Recipe
{
    public class RecipeStatusHistory
    {
        public DateTime? date { get; set; }
        public string? author { get; set; }
        public int? statusId { get; set; }
        public string? statusNameOld { get; set; }
        public string? statusColorOld { get; set; }
        public string? statusNameNew { get; set; }
        public string? statusColorNew { get; set; }
        public string? statusOld { get; set; }
        public string? statusNew { get; set; } = null!;
        public string? comment { get; set; }
    }
}
