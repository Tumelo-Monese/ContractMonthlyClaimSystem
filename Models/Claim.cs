using System.ComponentModel.DataAnnotations;

namespace ContractMonthlyClaimSystem.Models
{
    public enum ClaimStatus
    {
        Draft,
        Submitted,
        Verified,
        Approved,
        Rejected,
        Settled
    }

    public class Claim
    {
        public int ClaimId { get; set; }
        public int ContractId { get; set; }
        
        [Required(ErrorMessage = "Please select a month")]
        [Range(1, 12, ErrorMessage = "Please select a valid month")]
        public int Month { get; set; }
        
        [Required(ErrorMessage = "Please enter a year")]
        [Range(2020, 2100, ErrorMessage = "Year must be between 2020 and 2100")]
        public int Year { get; set; }
        
        public ClaimStatus Status { get; set; }
        public DateTime? SubmittedOn { get; set; }
        
        [Required(ErrorMessage = "Please enter total hours worked")]
        [Range(0.5, 200, ErrorMessage = "Hours must be between 0.5 and 200")]
        public decimal TotalHours { get; set; }
        
        [Required(ErrorMessage = "Please enter hourly rate")]
        [Range(1, 10000, ErrorMessage = "Hourly rate must be between R1 and R10,000")]
        public decimal HourlyRate { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        [StringLength(500, ErrorMessage = "Additional notes cannot exceed 500 characters")]
        public string? AdditionalNotes { get; set; }
        
        public int LecturerId { get; set; }
        public string LecturerName { get; set; } = string.Empty;
        
        // Navigation property for documents
        public List<Document> Documents { get; set; } = new List<Document>();
    }
}


