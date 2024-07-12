using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorApilo.Server.Migrations.SystemstData
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    REFRESH_TOKEN_EXP = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    SYNC_ORDERS_MIN = table.Column<int>(type: "INTEGER", nullable: false),
                    SYNC_STOCKS_MIN = table.Column<int>(type: "INTEGER", nullable: false),
                    LAST_UPDATED_AT = table.Column<DateTime>(type: "TIMESTAMP", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XXX_APILO_CONFIG", x => x.ID_CONFIG);
                });

            migrationBuilder.CreateTable(
                name: "XXX_APILO_DATABASE",
                columns: table => new
                {
                    ID_DATABASE = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    ID_CONFIG = table.Column<int>(type: "INTEGER", nullable: false),
                    CONNECTION_STRING = table.Column<string>(type: "VARCHAR(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XXX_APILO_DATABASE", x => x.ID_DATABASE);
                    table.ForeignKey(
                        name: "FK_XXX_APILO_DATABASE_XXX_APIL~",
                        column: x => x.ID_CONFIG,
                        principalTable: "XXX_APILO_CONFIG",
                        principalColumn: "ID_CONFIG",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_XXX_APILO_DATABASE_ID_CONFIG",
                table: "XXX_APILO_DATABASE",
                column: "ID_CONFIG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "XXX_APILO_DATABASE");

            migrationBuilder.DropTable(
                name: "XXX_APILO_CONFIG");
        }
    }
}
