namespace ContractMonthlyClaimSystem.Models
{
    public enum ClaimStatus
    {
        Draft,
        Submitted,
        Verified,
        Approved,
        Settled
    }

    public class Claim
    {
        public int ClaimId { get; set; }
        public int ContractId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public ClaimStatus Status { get; set; }
        public DateTime? SubmittedOn { get; set; }
    }
}


