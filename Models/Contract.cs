namespace ContractMonthlyClaimSystem.Models
{
    public class Contract
    {
        public int ContractId { get; set; }
        public int UserId { get; set; }
        public int ProgrammeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal HourlyRate { get; set; }
    }
}


