using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_reservations_UserId",
                table: "reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_parkings_OwnerId",
                table: "parkings",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_parkings_AspNetUsers_OwnerId",
                table: "parkings",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_AspNetUsers_UserId",
                table: "reservations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_parkings_AspNetUsers_OwnerId",
                table: "parkings");

            migrationBuilder.DropForeignKey(
                name: "FK_reservations_AspNetUsers_UserId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_UserId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_parkings_OwnerId",
                table: "parkings");
        }
    }
}
