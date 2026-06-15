using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atlasCDCPayroll.Migrations
{
    /// <inheritdoc />
    public partial class FixSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Designation = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Payslips",
                columns: table => new
                {
                    PayslipId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthName = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: false),
                    GeneratedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "TEXT", nullable: false),
                    NoOfLeaves = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaryPerDay = table.Column<decimal>(type: "TEXT", nullable: false),
                    DeductionForLeaves = table.Column<decimal>(type: "TEXT", nullable: false),
                    NetSalary = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payslips", x => x.PayslipId);
                    table.ForeignKey(
                        name: "FK_Payslips_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "Designation", "Email", "Level", "Name", "Password", "Phone", "Username" },
                values: new object[] { 101, "Staff", "ram@company.com", "1", "Ram Employee", "ram123", "555-0192", "ram" });

            migrationBuilder.InsertData(
                table: "Payslips",
                columns: new[] { "PayslipId", "BasicSalary", "DeductionForLeaves", "EmployeeId", "GeneratedOn", "Month", "MonthName", "NetSalary", "NoOfLeaves", "SalaryPerDay", "Year" },
                values: new object[] { 1, 50000m, 0m, 101, new DateTime(2026, 6, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 6, "June", 50000m, 0, 1666.67m, 2026 });

            migrationBuilder.CreateIndex(
                name: "IX_Payslips_EmployeeId",
                table: "Payslips",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payslips");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
