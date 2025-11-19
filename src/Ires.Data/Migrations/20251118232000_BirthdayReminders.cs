using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ires.Data.Migrations
{
    /// <inheritdoc />
    public partial class BirthdayReminders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BirthDayOfYear",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthDayOfYear",
                table: "People");
        }
    }
}
