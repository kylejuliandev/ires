using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ires.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Street = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    GivenName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    FamilyName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nickname = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    DateOfBirth = table.Column<DateOnly>(type: "TEXT", nullable: false, defaultValue: new DateOnly(1, 1, 1))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false, defaultValue: 0),
                    Value = table.Column<string>(type: "TEXT", maxLength: 320, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetails_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleNotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleNotes_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonAddresses",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddressId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AddedOn = table.Column<DateTimeOffset>(type: "TEXT", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonAddresses", x => new { x.AddressId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_PersonAddresses_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonAddresses_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_PersonId_Type",
                table: "ContactDetails",
                columns: new[] { "PersonId", "Type" });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleNotes_PersonId",
                table: "PeopleNotes",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonAddresses_PersonId",
                table: "PersonAddresses",
                column: "PersonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "PeopleNotes");

            migrationBuilder.DropTable(
                name: "PersonAddresses");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
