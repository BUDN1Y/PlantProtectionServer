namespace PlantProtectionServer.Models.Recipe
{
    public class RecipesData
    {
        public int id { get; set; }
        public int productId { get; set; }
        public DateOnly? creationDate { get; set; }
        public int version { get; set; }
        public string? comments { get; set; }
        public int statusId { get; set; }
        public int authorId { get; set; }
        public string authorName { get; set; } = string.Empty;
        public string statusName { get; set; } = null!;
        public string statusColor { get; set; } = null!;
    }
}
