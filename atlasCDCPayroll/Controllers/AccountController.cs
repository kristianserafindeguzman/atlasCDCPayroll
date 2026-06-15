using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using atlasCDCPayroll.Data;
using System.Security.Claims;

namespace atlasCDCPayroll.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Hardcoded legacy admins mapping
            string? role = null;
            string? employeeName = null;

            if (username == "admin" && password == "payroll123")
            {
                role = "Administrator";
                employeeName = "System Admin";
            }
            else if (username == "manager" && password == "payroll123")
            {
                role = "Manager";
                employeeName = "System Manager";
            }
            else
            {
                var user = await _context.Employees.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
                if (user != null)
                {
                    role = "Employee";
                    employeeName = user.Name;

                    // We can store EmployeeId in claims
                }
            }

            if (role != null && employeeName != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, username),
                    new Claim(ClaimTypes.Name, employeeName),
                    new Claim(ClaimTypes.Role, role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid username or password.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}