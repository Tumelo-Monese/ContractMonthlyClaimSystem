namespace ContractMonthlyClaimSystem.Models
{
    public class ClaimLineItem
    {
        public int LineItemId { get; set; }
        public int ClaimId { get; set; }
        public DateTime Date { get; set; }
        public decimal HoursWorked { get; set; }
        public string WorkType { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}


