using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Services;


namespace ContractMonthlyClaimSystem.Controllers
{
    [Authorize(Roles = "ProgrammeCoordinator,AcademicManager")]
    public class ClaimsApprovalController : Controller
    {
        private readonly IClaimservice _claimService;

        public ClaimsApprovalController(IClaimservice claimService)
        {
            _claimService = claimService;
        }

        // Displays list of claims with status Verified for review
        public IActionResult Index()
        {
            var pendingClaims = _claimService.GetClaimsByStatus(ClaimStatus.Verified).ToList();
            return View(pendingClaims);
        }

        // Approves a claim, changing status to Approved
        [HttpPost]
        public IActionResult Approve(int claimId)
        {
            var claim = _claimService.GetClaimById(claimId);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = ClaimStatus.Approved;
            _claimService.UpdateClaim(claim);

            // TODO: Add notification logic if needed

            return RedirectToAction(nameof(Index));
        }

        // Rejects a claim, changing status to Rejected and optionally saves a reason
        [HttpPost]
        public IActionResult Reject(int claimId, string reason)
        {
            var claim = _claimService.GetClaimById(claimId);
            if (claim == null)
            {
                return NotFound();
            }

            claim.Status = ClaimStatus.Rejected;

            // Optionally save rejection reason (extend Claim model and repository as needed)
             //claim.RejectionReason = reason;

            _claimService.UpdateClaim(claim);

            return RedirectToAction(nameof(Index));
        }
    }
}
