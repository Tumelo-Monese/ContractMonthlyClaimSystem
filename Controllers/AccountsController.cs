using ContractMonthlyClaimSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContractMonthlyClaimSystem.Controllers
{ 
public class AccountsController : Controller
{
    private static List<User> _users = new();

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    public IActionResult Register(Registration model)
    {
        if (ModelState.IsValid)
        {
            if (_users.Any(u => u.FullName == $"{model.FirstName} {model.LastName}"))
            {
                ModelState.AddModelError("", "User exists");
                return View(model);
            }

            var user = new User
            {
                UserId = _users.Count + 1,
                FullName = $"{model.FirstName} {model.LastName}",
                Role = model.Role,
                Password = model.Password
            };
            _users.Add(user);

            HttpContext.Session.SetString("UserFullName", user.FullName);
            HttpContext.Session.SetString("UserRole", user.Role.ToString());

            return RedirectToRoleStartPage(user.Role);
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(Login model)
    {
        if (ModelState.IsValid)
        {
            var user = _users.FirstOrDefault(u => u.FullName == model.FullName && u.Password == model.Password);

            if (user is not null)
            {
                HttpContext.Session.SetString("UserFullName", user.FullName);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());
                return RedirectToRoleStartPage(user.Role);
            }
            ModelState.AddModelError("", "Invalid login");
        }
        return View(model);
    }

    private IActionResult RedirectToRoleStartPage(UserRole role)
    {
        return role switch
        {
            UserRole.Lecturer => RedirectToAction("Index", "Claims"),
            UserRole.ProgrammeCoordinator => RedirectToAction("Verify", "Reviews"),
            UserRole.AcademicManager => RedirectToAction("Approve", "Reviews"),
            _ => RedirectToAction("Index", "Home"),
        };
    }
} }
