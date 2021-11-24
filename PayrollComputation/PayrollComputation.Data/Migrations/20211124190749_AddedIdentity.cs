using Microsoft.EntityFrameworkCore.Migrations;

namespace PayrollComputation.Data.Migrations
{
    public partial class AddedIdentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nino",
                table: "PaymentRecords",
                newName: "NiNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NiNo",
                table: "PaymentRecords",
                newName: "Nino");
        }
    }
}
