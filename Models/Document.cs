namespace ContractMonthlyClaimSystem.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        public int ClaimId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }
    }
}


