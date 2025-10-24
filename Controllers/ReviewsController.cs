using Microsoft.AspNetCore.Mvc;
using ContractMonthlyClaimSystem.Models;
using ContractMonthlyClaimSystem.Services;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IClaimservice _claimService;

        public ReviewsController(IClaimservice claimService)
        {
            _claimService = claimService;
        }

      
        public IActionResult Verify()
        {
            var pendingClaims = _claimService.GetClaimsByStatus(ClaimStatus.Submitted);
            return View(pendingClaims);
        }

        
        public IActionResult Approve()
        {
            var verifiedClaims = _claimService.GetClaimsByStatus(ClaimStatus.Verified);
            return View(verifiedClaims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int claimId, string action, string? comment = null)
        {
            var claim = _claimService.GetClaimById(claimId);

            if (claim == null)
            {
                TempData["ErrorMessage"] = "Claim not found.";
                return RedirectToAction("Verify");
            }

            if (action == "verify")
            {
                claim.Status = ClaimStatus.Verified;
                _claimService.UpdateClaim(claim);
                TempData["SuccessMessage"] = $"Claim #{claimId} has been verified and sent to Academic Manager for approval.";
            }
            else if (action == "reject")
            {
                claim.Status = ClaimStatus.Rejected;
                _claimService.UpdateClaim(claim);
                TempData["SuccessMessage"] = $"Claim #{claimId} has been rejected and returned to lecturer.";
            }

            return RedirectToAction("Verify");
        }

        [HttpPost]
        public IActionResult ApproveClaim(int claimId, string action, string? comment = null)
        {
            var claim = _claimService.GetClaimById(claimId);

            if (claim == null)
            {
                TempData["ErrorMessage"] = "Claim not found.";
                return RedirectToAction("Approve");
            }

            if (action == "approve")
            {
                claim.Status = ClaimStatus.Approved;
                _claimService.UpdateClaim(claim);
                TempData["SuccessMessage"] = $"Claim #{claimId} has been approved successfully.";
            }
            else if (action == "reject")
            {
                claim.Status = ClaimStatus.Rejected;
                _claimService.UpdateClaim(claim);
                TempData["SuccessMessage"] = $"Claim #{claimId} has been rejected and returned to lecturer.";
            }

            return RedirectToAction("Approve");
        }

        public IActionResult ClaimDetails(int id)
        {
            var claim = _claimService.GetClaimById(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View("~/Views/Claims/Details.cshtml", claim);
        }
    }
}