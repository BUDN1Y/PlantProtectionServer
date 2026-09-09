namespace PlantProtectionServer.Models.Recipe
{
    public class RawMaterialsData
    {
        public int id { get; set; }
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? category { get; set; }
        public string unit { get; set; } = null!;
    }
}
