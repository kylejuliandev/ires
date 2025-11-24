using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ires.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGender : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "People",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
