
using ContractMonthlyClaimSystem.Models;
namespace ContractMonthlyClaimSystem.Services
{
    public interface IClaimVerificationService
    {
        VerificationResult VerifyClaim(Claim claim);
    }

    public class VerificationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }

    public class ClaimVerificationService : IClaimVerificationService
    {
        public VerificationResult VerifyClaim(Claim claim)
        {
            var result = new VerificationResult { IsValid = true };

            if (claim.TotalHours < 0.5m || claim.TotalHours > 200m)
            {
                result.IsValid = false;
                result.Errors.Add("Total hours must be between 0.5 and 200.");
            }

            if (claim.HourlyRate < 1m || claim.HourlyRate > 10000m)
            {
                result.IsValid = false;
                result.Errors.Add("Hourly rate must be between R1 and R10,000.");
            }

            

            return result;
        }
    }
}