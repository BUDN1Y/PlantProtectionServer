namespace PlantProtectionServer.Models.Recipe
{
    public class CreateRecipe
    {
        public int productId { get; set; }
        public int version { get; set; }
        public int statusId { get; set; }
        public int authorId { get; set; }
        public DateOnly? creationDate { get; set; }
        public string? comments { get; set; }
        public RecipesDataGrid[]? componets { get; set; }
    }
}
