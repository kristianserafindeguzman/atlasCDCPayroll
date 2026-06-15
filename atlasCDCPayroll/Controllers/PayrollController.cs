using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using atlasCDCPayroll.Data;
using atlasCDCPayroll.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace atlasCDCPayroll.Controllers
{
    public class PayrollController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayrollController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Payroll/Dashboard (Dynamic landing layout for all 3 profiles)
        public IActionResult Dashboard()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole")))
                return RedirectToAction("Index", "Auth");
            return View();
        }

        // ==========================================
        // FEATURE AREA: INNER-OFFICE MESSAGING
        // ==========================================

        // GET: /Payroll/Messages
        public IActionResult Messages()
        {
            string sessionUser = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(sessionUser)) return RedirectToAction("Index", "Auth");

            // Filter message results down precisely to match targeted inbound identities
            var inbox = _context.Messages.Where(m => m.ReceiverName == sessionUser || m.ReceiverName == "Manager").ToList();
            return View(inbox);
        }

        // GET: /Payroll/Compose
        public IActionResult Compose()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserRole"))) return RedirectToAction("Index", "Auth");

            // Build out selection properties for routing data to targeting select menus
            ViewBag.Employees = _context.Employees.Select(e => e.Name).ToList();
            return View();
        }

        // POST: /Payroll/SendMessage
        [HttpPost]
        public IActionResult SendMessage(string receiverName, string messageText)
        {
            var msg = new MessageModel
            {
                SenderName = HttpContext.Session.GetString("Username") ?? "System",
                ReceiverName = receiverName,
                MessageText = messageText
            };
            _context.Messages.Add(msg);
            _context.SaveChanges();
            return RedirectToAction("Messages");
        }

        // ==========================================
        // FEATURE AREA: SALARY COMPUTATIONS & LEDGERS
        // ==========================================

        // GET: /Payroll/Payslips (Manager-facing master calculation desk)
        public IActionResult Payslips()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager") return RedirectToAction("Dashboard");

            ViewBag.Employees = _context.Employees.Select(e => e.Name).ToList();
            return View(new List<Payslip>());
        }

        // POST: /Payroll/GetEmployeePayslips
        [HttpPost]
        public IActionResult GetEmployeePayslips(string employeeName)
        {
            ViewBag.Employees = _context.Employees.Select(e => e.Name).ToList();
            ViewBag.SelectedEmployee = employeeName;

            var history = _context.Payslips.Where(p => p.EmployeeName == employeeName).ToList();
            return View("Payslips", history);
        }

        // GET: /Payroll/GeneratePayslipForm
        public IActionResult GeneratePayslipForm(string name)
        {
            ViewBag.EmployeeName = name;
            return View();
        }

        // POST: /Payroll/SavePayslip
        [HttpPost]
        public IActionResult SavePayslip(string employeeName, string month, string year, decimal basicSalary, int leaves)
        {
            // Core Payroll Arithmetic logic setup block
            decimal dayRate = Math.Round(basicSalary / 31, 2);
            decimal leafDeductions = Math.Round(leaves * dayRate, 2);
            decimal dynamicNet = basicSalary - leafDeductions;

            var slip = new Payslip
            {
                EmployeeName = employeeName,
                MonthAndYear = month + " " + year,
                BasicSalary = basicSalary,
                Leaves = leaves,
                SalaryPerDay = dayRate,
                Deductions = leafDeductions,
                NetSalary = dynamicNet
            };

            _context.Payslips.Add(slip);
            _context.SaveChanges();
            return RedirectToAction("Payslips");
        }

        // GET: /Payroll/PersonalPayslips (Employee Personal Portal history grid)
        public IActionResult PersonalPayslips()
        {
            string loggedUser = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(loggedUser)) return RedirectToAction("Index", "Auth");

            var personalHistory = _context.Payslips.Where(p => p.EmployeeName == loggedUser).ToList();
            return View(personalHistory);
        }

        // GET: /Payroll/PrintPayslip/5 (Renders clean report print bounds context canvas)
        public IActionResult PrintPayslip(int id)
        {
            var item = _context.Payslips.Find(id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}