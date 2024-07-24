using System;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IntegratorApilo.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "XXX_APILO_ORDER_STATUS",
                columns: table => new
                {
                    ORDER_STATUS_ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn),
                    SHOP_ID = table.Column<int>(type: "INTEGER", nullable: false),
                    KEY = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    NAME = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "VARCHAR(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XXX_APILO_ORDER_STATUS", x => x.ORDER_STATUS_ID);
                    table.ForeignKey(
                        name: "FK_XXX_APILO_ORDER_STATUS_XXX_~",
                        column: x => x.SHOP_ID,
                        principalTable: "XXX_APILO_SHOP",
                        principalColumn: "ID_SHOP",
                        onDelete: ReferentialAction.Cascade);
                });

         

            migrationBuilder.CreateIndex(
                name: "IX_XXX_APILO_ORDER_STATUS_SHOP~",
                table: "XXX_APILO_ORDER_STATUS",
                column: "SHOP_ID");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "XXX_APILO_ORDER_STATUS");


        }
    }
}
