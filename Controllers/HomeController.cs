using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            var userRole = HttpContext.Session.GetString("UserRole");
            if (!string.IsNullOrEmpty(userRole))
            {
               
                return userRole switch
                {
                    "Lecturer" => RedirectToAction("Index", "Claims"),
                    "ProgrammeCoordinator" => RedirectToAction("Verify", "Reviews"),
                    "AcademicManager" => RedirectToAction("Approve", "Reviews"),
                    _ => View()
                };
            }

            return View();
        }
    }
}


