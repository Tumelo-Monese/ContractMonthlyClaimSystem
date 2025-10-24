using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public class Document
    {
        public int DocumentId { get; set; }
        
        [Required]
        public int ClaimId { get; set; }
        
        [Required]
        [StringLength(255, ErrorMessage = "File name cannot be over 255 characters")]
        public string FileName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500, ErrorMessage = "File path cannot be over 500 characters")]
        public string FilePath { get; set; } = string.Empty;
        
        [Required]
        public DateTime UploadedOn { get; set; }
        
        [StringLength(50)]
        public string FileType { get; set; } = string.Empty;
        
        public long FileSize { get; set; }
        
        [StringLength(100)]
        public string MimeType { get; set; } = string.Empty;
     
        public Claim? Claim { get; set; }
    }
}


