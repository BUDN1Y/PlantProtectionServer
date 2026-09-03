using PlantProtectionServer.ModelsDB;

namespace PlantProtectionServer.Models
{
    public class ConfirmationProduct
    {
        public string? recipe { get; set; }
        public string? techcard { get; set; }
        public int? id { get; set; }
        public int? oldStatus { get; set; }
        public string? oldCode { get; set; }
        public string code { get; set; } = null!;
        public string name { get; set; } = null!;
        public string? type { get; set; }
        public string? releaseForm { get; set; } = null!;
        public string comment { get; set; } = null!;
        public int status { get; set; }

    }
}
