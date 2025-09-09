namespace ContractMonthlyClaimSystem.Models
{
    public enum ApprovalAction
    {
        Submitted,
        Verified,
        Approved,
        Rejected
    }

    public class ApprovalLog
    {
        public int ApprovalLogId { get; set; }
        public int ClaimId { get; set; }
        public int ActionByUserId { get; set; }
        public ApprovalAction ActionType { get; set; }
        public DateTime ActionOn { get; set; }
        public string? Comment { get; set; }
    }
}


