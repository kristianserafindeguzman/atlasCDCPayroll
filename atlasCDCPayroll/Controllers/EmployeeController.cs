using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using atlasCDCPayroll.Data;
using atlasCDCPayroll.Models;
using System.Linq;

namespace atlasCDCPayroll.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Employee/Index (Handles both Employee Management & Employee Directory views)
        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
                return RedirectToAction("Index", "Auth");

            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // GET: /Employee/Create (Displays the Add Employee Form to Admins)
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Index", "Employee");

            // Compute the next automatic visual sequence ID to mirror the interface screenshot
            ViewBag.NextID = _context.Employees.Any() ? _context.Employees.Max(e => e.EmployeeID) + 1 : 1;
            return View();
        }

        // POST: /Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(emp);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            // Re-calculate hint ID properties if server-side validation rules drop execution traffic back to the form
            ViewBag.NextID = _context.Employees.Any() ? _context.Employees.Max(e => e.EmployeeID) + 1 : 1;
            return View(emp);
        }

        // GET: /Employee/Delete/5
        public IActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Index");

            var target = _context.Employees.Find(id);
            if (target != null)
            {
                _context.Employees.Remove(target);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
