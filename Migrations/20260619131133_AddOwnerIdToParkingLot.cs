using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIdToParkingLot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "parkings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: null);

            migrationBuilder.UpdateData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_reservations_ParkingId",
                table: "reservations",
                column: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_reservations_parkings_ParkingId",
                table: "reservations",
                column: "ParkingId",
                principalTable: "parkings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reservations_parkings_ParkingId",
                table: "reservations");

            migrationBuilder.DropIndex(
                name: "IX_reservations_ParkingId",
                table: "reservations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "parkings");
        }
    }
}
