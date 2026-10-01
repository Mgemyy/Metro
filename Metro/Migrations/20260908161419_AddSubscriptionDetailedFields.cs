using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Metro.Migrations
{
    /// <inheritdoc />
    public partial class AddSubscriptionDetailedFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployerName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HrLetterPath",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstTime",
                table: "Subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUniversityStudent",
                table: "Subscriptions",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolOrUniversityName",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StudentProofPath",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserCategory",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployerName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "HrLetterPath",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsFirstTime",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "IsUniversityStudent",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SchoolOrUniversityName",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "StudentProofPath",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserCategory",
                table: "Subscriptions");
        }
    }
}
