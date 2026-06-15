using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace atlasCDCPayroll.Migrations
{
    /// <inheritdoc />
    public partial class StaticSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payslips",
                keyColumn: "PayslipId",
                keyValue: 1,
                column: "GeneratedOn",
                value: new DateTime(2026, 6, 15, 12, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Payslips",
                keyColumn: "PayslipId",
                keyValue: 1,
                column: "GeneratedOn",
                value: new DateTime(2026, 6, 15, 12, 56, 9, 157, DateTimeKind.Local).AddTicks(3273));
        }
    }
}
