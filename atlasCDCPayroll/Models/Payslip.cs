using System;
using System.ComponentModel.DataAnnotations;

namespace atlasCDCPayroll.Models
{
    public class Payslip
    {
        [Key]
        public int PayslipID { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string MonthAndYear { get; set; }

        public DateTime GeneratedOn { get; set; } = DateTime.Now;

        [Required]
        public decimal BasicSalary { get; set; }

        [Required]
        public int Leaves { get; set; }

        [Required]
        public decimal SalaryPerDay { get; set; }

        [Required]
        public decimal Deductions { get; set; }

        [Required]
        public decimal NetSalary { get; set; }
    }
}