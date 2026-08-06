using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Parking.Migrations
{
    /// <inheritdoc />
    public partial class RemoveParkingSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "parkings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "parkings",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "parkings",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "parkings",
                columns: new[] { "Id", "Location", "Name", "OwnerId", "PricePerHour", "ReservedSpots", "TotalSpots" },
                values: new object[,]
                {
                    { 1, "Chalandri, Grammou 12", "Parking A", null, 2.50m, 0, 10 },
                    { 2, "Chalandri, Bakogianni 10", "Parking B", null, 2.00m, 0, 20 },
                    { 3, "Chalandri, Attikis 8", "Parking C", null, 1.50m, 0, 15 }
                });
        }
    }
}
