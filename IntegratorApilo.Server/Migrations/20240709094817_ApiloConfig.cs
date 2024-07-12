using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorApilo.Server.Migrations
{
    /// <inheritdoc />
    public partial class ApiloConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XXX_APILO_CONFIG",
                columns: table => new
                {
                    ID_CONFIG = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    APP_NAME = table.Column<string>(type: "VARCHAR(60)", maxLength: 60, nullable: false),
                    APP_DESCRIPTION = table.Column<string>(type: "VARCHAR(250)", maxLength: 250, nullable: true),
                    API_ADDRESS = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    CLIENT_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    CLIENT_SECRET = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    AUTH_TOKEN = table.Column<string>(type: "VARCHAR(200)", maxLength: 200, nullable: false),
                    ACCESS_TOKEN = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: true),
                    ACCESS_TOKEN_EXP = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    REFRESH_TOKEN = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: true),
                    REFRESH_TOKEN_EXP = table.Column<DateTime>(type: "TIMESTAMP", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XXX_APILO_CONFIG", x => x.ID_CONFIG);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XXX_APILO_CONFIG");
        }
    }
}
