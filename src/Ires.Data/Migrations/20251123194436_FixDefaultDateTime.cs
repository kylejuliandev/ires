using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ires.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixDefaultDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AddedOn",
                table: "PersonAddresses",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT",
                oldDefaultValueSql: "now()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "AddedOn",
                table: "PersonAddresses",
                type: "TEXT",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTimeOffset),
                oldType: "TEXT");
        }
    }
}
