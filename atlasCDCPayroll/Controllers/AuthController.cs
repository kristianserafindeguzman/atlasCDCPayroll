using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using atlasCDCPayroll.Data;
using atlasCDCPayroll.Models; // ADDED THIS CRITICAL LINE TO CLEAR RECURSIVE AMBIGUITY
using System.Linq;

namespace atlasCDCPayroll.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Auth/Index (The main Sign-In page)
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
            {
                return RedirectToAction("Dashboard", "Payroll");
            }
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            // Now fully qualified via model using statements
            var account = _context.Employees.FirstOrDefault(e => e.Username == username && e.Password == password);

            if (account != null)
            {
                HttpContext.Session.SetString("Username", account.Name);

                string role = account.Level == 1 ? "Admin" :
                             (account.Level == 2 || account.Level == 3 ? "Manager" : "Employee");
                HttpContext.Session.SetString("UserRole", role);

                return RedirectToAction("Dashboard", "Payroll");
            }

            ModelState.AddModelError("", "Authentication credentials failed verification check.");
            return View("Index");
        }

        // GET: /Auth/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}