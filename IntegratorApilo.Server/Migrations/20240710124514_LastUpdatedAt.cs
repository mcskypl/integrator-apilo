using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorApilo.Server.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LAST_UPDATED_AT",
                table: "XXX_APILO_CONFIG",
                type: "TIMESTAMP",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LAST_UPDATED_AT",
                table: "XXX_APILO_CONFIG");
        }
    }
}
