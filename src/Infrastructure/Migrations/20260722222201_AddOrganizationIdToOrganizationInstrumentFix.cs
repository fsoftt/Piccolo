using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationIdToOrganizationInstrumentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments",
                column: "OrganizationId",
                unique: true);
        }
    }
}
