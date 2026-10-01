using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metro.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeProfileAndAuditFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdminNotes",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewedByAdminId",
                table: "Subscriptions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeProfiles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Department = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    OfficeLocation = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfiles", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_EmployeeProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_ReviewedByAdminId",
                table: "Subscriptions",
                column: "ReviewedByAdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_AspNetUsers_ReviewedByAdminId",
                table: "Subscriptions",
                column: "ReviewedByAdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_AspNetUsers_ReviewedByAdminId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "EmployeeProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_ReviewedByAdminId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "AdminNotes",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "ReviewedByAdminId",
                table: "Subscriptions");
        }
    }
}
