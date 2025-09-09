namespace ContractMonthlyClaimSystem.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public int EntityId { get; set; }
        public string Action { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}


