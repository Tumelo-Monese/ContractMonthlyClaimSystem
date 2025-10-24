using ContractMonthlyClaimSystem.Models;

namespace ContractMonthlyClaimSystem.Services
{
    public class ClaimService : IClaimservice
    {
        private static List<Claim> _claims = new List<Claim>(); 
        private static int _nextClaimId = 1; 


        public List<Claim> GetAllClaims()
        {
            return _claims;
        }

        public Claim? GetClaimById(int id)
        {
            return _claims.FirstOrDefault(c => c.ClaimId == id);
        }

        public void AddClaim(Claim claim)
        {
            claim.ClaimId = _nextClaimId++;
            _claims.Add(claim);
        }

        public void UpdateClaim(Claim claim)
        {
            var existingClaim = _claims.FirstOrDefault(c => c.ClaimId == claim.ClaimId);
            if (existingClaim != null)
            {
                var index = _claims.IndexOf(existingClaim);
                _claims[index] = claim;
            }
        }

        public List<Claim> GetClaimsByStatus(ClaimStatus status)
        {
            return _claims.Where(c => c.Status == status).ToList();
        }

        public List<Claim> GetClaimsByLecturerId(int lecturerId)
        {
            return _claims.Where(c => c.LecturerId == lecturerId).ToList();
        }
    }
}