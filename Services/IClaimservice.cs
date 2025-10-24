using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Services
{
    public interface IClaimservice
    {
        List<Claim> GetAllClaims();
        Claim? GetClaimById(int id);
        void AddClaim(Claim claim);
        void UpdateClaim(Claim claim);
        List<Claim> GetClaimsByStatus(ClaimStatus status);
        List<Claim> GetClaimsByLecturerId(int lecturerId);
    }
}
