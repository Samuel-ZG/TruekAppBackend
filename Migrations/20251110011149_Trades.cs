using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruekAppAPI.Migrations
{
    /// <inheritdoc />
    public partial class Trades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OfferedTrueCoins",
                table: "Trades",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "RequestedTrueCoins",
                table: "Trades",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OfferedTrueCoins",
                table: "Trades");

            migrationBuilder.DropColumn(
                name: "RequestedTrueCoins",
                table: "Trades");
        }
    }
}
