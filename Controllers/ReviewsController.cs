using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class ReviewsController : Controller
    {
        // Coordinator inbox
        public IActionResult Verify()
        {
            return View();
        }

        // Manager inbox
        public IActionResult Approve()
        {
            return View();
        }
    }
}


