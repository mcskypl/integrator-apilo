using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorApilo.Server.Migrations.SystemstData
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ID_MAGAZYN_STOCKS",
                table: "XXX_APILO_CONFIG",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ID_MAGAZYN_STOCKS",
                table: "XXX_APILO_CONFIG");
        }
    }
}
