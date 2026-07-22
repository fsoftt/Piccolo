using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOrganizationIdToOrganizationInstrument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrganizationInstruments_OrganizationId_Name",
                table: "OrganizationInstruments");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "OrganizationInstruments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments",
                column: "OrganizationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrganizationInstruments_OrganizationId",
                table: "OrganizationInstruments");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrganizationId",
                table: "OrganizationInstruments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationInstruments_OrganizationId_Name",
                table: "OrganizationInstruments",
                columns: new[] { "OrganizationId", "Name" },
                unique: true,
                filter: "[OrganizationId] IS NOT NULL");
        }
    }
}
