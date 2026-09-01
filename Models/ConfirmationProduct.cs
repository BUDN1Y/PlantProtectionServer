namespace PlantProtectionServer.Models
{
    public class ConfirmationProduct
    {
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? type { get; set; }
        public string? releaseForm { get; set; } = null!;
        public string comment { get; set; } = null!;
        public int status { get; set; }

    }
}
