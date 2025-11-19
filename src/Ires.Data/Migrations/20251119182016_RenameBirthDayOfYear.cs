using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ires.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameBirthDayOfYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthDayOfYear",
                table: "People",
                newName: "BirthdayDayOfYear");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BirthdayDayOfYear",
                table: "People",
                newName: "BirthDayOfYear");
        }
    }
}
