using System.ComponentModel.DataAnnotations;

namespace atlasCDCPayroll.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Name field cannot be left blank.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone contact field is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Email address field is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Designation/Job Title is required.")]
        public string Designation { get; set; }

        [Required]
        [Range(1, 4, ErrorMessage = "Level must be 1 (Admin), 2 or 3 (Manager), or 4 (Employee).")]
        public int Level { get; set; }

        [Required(ErrorMessage = "Username is required for signing in.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required for signing in.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}