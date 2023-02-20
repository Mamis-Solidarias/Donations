using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MamisSolidarias.WebAPI.Donations.Migrations
{
    /// <inheritdoc />
    public partial class AddedSearchableIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchableMotive",
                table: "MonetaryDonations",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_MonetaryDonations_SearchableMotive",
                table: "MonetaryDonations",
                column: "SearchableMotive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MonetaryDonations_SearchableMotive",
                table: "MonetaryDonations");

            migrationBuilder.DropColumn(
                name: "SearchableMotive",
                table: "MonetaryDonations");
        }
    }
}
