using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntegratorApilo.Server.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdatedAt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SYNC_ORDERS_MIN",
                table: "XXX_APILO_CONFIG",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SYNC_STOCKS_MIN",
                table: "XXX_APILO_CONFIG",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SYNC_ORDERS_MIN",
                table: "XXX_APILO_CONFIG");

            migrationBuilder.DropColumn(
                name: "SYNC_STOCKS_MIN",
                table: "XXX_APILO_CONFIG");
        }
    }
}
