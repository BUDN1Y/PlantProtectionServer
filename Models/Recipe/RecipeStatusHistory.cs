namespace PlantProtectionServer.Models.Recipe
{
    public class RecipeStatusHistory
    {
        public DateTime? date { get; set; }
        public string? author { get; set; }
        public string? statusOld { get; set; }
        public string? statusNew { get; set; } = null!;
        public string? comment { get; set; }
    }
}
