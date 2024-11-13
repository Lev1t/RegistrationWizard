using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RegistrationWizard.Server.Infrastructure.Migrations;

/// <inheritdoc />
public partial class init : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Countries",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Countries", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Provinces",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                CountryId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Provinces", x => x.Id);
                table.ForeignKey(
                    name: "FK_Provinces_Countries_CountryId",
                    column: x => x.CountryId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                IsAgreeToWorkForFood = table.Column<bool>(type: "INTEGER", nullable: false),
                CountryId = table.Column<int>(type: "INTEGER", nullable: false),
                ProvinceId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Countries_CountryId",
                    column: x => x.CountryId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Users_Provinces_ProvinceId",
                    column: x => x.ProvinceId,
                    principalTable: "Provinces",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserLogins",
            columns: table => new
            {
                Id = table.Column<int>(type: "INTEGER", nullable: false)
                    .Annotation("Sqlite:Autoincrement", true),
                Login = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                PasswordSalt = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                UserId = table.Column<int>(type: "INTEGER", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserLogins", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserLogins_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "Countries",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Australia" },
                { 2, "Brasilia" },
                { 3, "Mexico" }
            });

        migrationBuilder.InsertData(
            table: "Provinces",
            columns: new[] { "Id", "CountryId", "Name" },
            values: new object[,]
            {
                { 1, 1, "Province 1" },
                { 2, 1, "Province 2" },
                { 3, 2, "Province 3" },
                { 4, 2, "Province 4" },
                { 5, 3, "Province 5" },
                { 6, 3, "Province 6" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Provinces_CountryId",
            table: "Provinces",
            column: "CountryId");

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_Login",
            table: "UserLogins",
            column: "Login",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserLogins_UserId",
            table: "UserLogins",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_CountryId",
            table: "Users",
            column: "CountryId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_ProvinceId",
            table: "Users",
            column: "ProvinceId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserLogins");

        migrationBuilder.DropTable(
            name: "Users");

        migrationBuilder.DropTable(
            name: "Provinces");

        migrationBuilder.DropTable(
            name: "Countries");
    }
}
