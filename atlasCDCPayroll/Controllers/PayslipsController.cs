using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using atlasCDCPayroll.Data;
using System.Security.Claims;

namespace atlasCDCPayroll.Controllers
{
    [Authorize]
    public class PayslipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PayslipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> MyPayslips()
        {
            // Find logged in username (ram)
            var username = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Find employee matching ID 
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Username == username);
            if (employee == null) return NotFound();

            var userPayslips = await _context.Payslips
                .Where(p => p.EmployeeId == employee.EmployeeId)
                .ToListAsync();

            return View(userPayslips);
        }

        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Print(int id)
        {
            var payslip = await _context.Payslips
                .Include(p => p.Employee)
                .FirstOrDefaultAsync(p => p.PayslipId == id);

            if (payslip == null) return NotFound();

            return View(payslip); // Matches the print.aspx template design
        }
    }
}