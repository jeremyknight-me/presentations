using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataPersistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lookups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookups", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 1, true, "virtual array" },
                    { 2, true, "virtual system" },
                    { 3, true, "solid state bandwidth" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "neural hard drive" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 5, true, "multi-byte driver" },
                    { 6, true, "1080p interface" },
                    { 7, true, "back-end application" },
                    { 8, true, "solid state monitor" },
                    { 9, true, "1080p matrix" },
                    { 10, true, "multi-byte protocol" },
                    { 11, true, "bluetooth matrix" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 12, "cross-platform card" },
                    { 13, "primary driver" },
                    { 14, "solid state feed" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 15, true, "back-end pixel" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 16, "virtual system" },
                    { 17, "haptic program" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 18, true, "1080p system" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 19, "primary interface" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 20, true, "neural firewall" },
                    { 21, true, "auxiliary array" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 22, "optical microchip" },
                    { 23, "multi-byte program" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 24, true, "virtual alarm" },
                    { 25, true, "open-source program" },
                    { 26, true, "digital application" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 27, "digital system" },
                    { 28, "haptic protocol" },
                    { 29, "1080p circuit" },
                    { 30, "mobile port" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 31, true, "cross-platform port" },
                    { 32, true, "cross-platform circuit" },
                    { 33, true, "mobile card" },
                    { 34, true, "redundant application" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 35, "digital microchip" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 36, true, "open-source firewall" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 37, "cross-platform circuit" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 38, true, "online alarm" },
                    { 39, true, "optical firewall" },
                    { 40, true, "back-end bandwidth" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 41, "auxiliary pixel" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 42, true, "neural matrix" },
                    { 43, true, "optical circuit" },
                    { 44, true, "1080p program" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 45, "wireless monitor" },
                    { 46, "cross-platform card" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 47, true, "virtual sensor" },
                    { 48, true, "cross-platform alarm" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 49, "open-source circuit" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 50, true, "redundant sensor" },
                    { 51, true, "primary transmitter" },
                    { 52, true, "back-end interface" },
                    { 53, true, "digital array" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 54, "back-end interface" },
                    { 55, "optical pixel" },
                    { 56, "digital monitor" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 57, true, "redundant sensor" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 58, "redundant bus" },
                    { 59, "solid state feed" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 60, true, "multi-byte hard drive" },
                    { 61, true, "neural sensor" },
                    { 62, true, "mobile feed" },
                    { 63, true, "open-source pixel" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 64, "haptic microchip" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 65, true, "wireless panel" },
                    { 66, true, "1080p sensor" },
                    { 67, true, "1080p bandwidth" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 68, "wireless matrix" },
                    { 69, "digital alarm" },
                    { 70, "haptic microchip" },
                    { 71, "open-source alarm" },
                    { 72, "primary interface" },
                    { 73, "digital bus" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 74, true, "digital circuit" },
                    { 75, true, "multi-byte circuit" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 76, "open-source monitor" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 77, true, "multi-byte panel" },
                    { 78, true, "solid state transmitter" },
                    { 79, true, "solid state protocol" },
                    { 80, true, "redundant transmitter" },
                    { 81, true, "online monitor" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 82, "bluetooth port" },
                    { 83, "back-end hard drive" },
                    { 84, "back-end pixel" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 85, true, "back-end firewall" },
                    { 86, true, "auxiliary panel" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 87, "wireless capacitor" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 88, true, "open-source matrix" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 89, "haptic array" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { 90, true, "cross-platform microchip" },
                    { 91, true, "redundant monitor" },
                    { 92, true, "optical monitor" }
                });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 93, "1080p capacitor" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 94, true, "1080p panel" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 95, "mobile system" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[] { 96, true, "multi-byte application" });

            migrationBuilder.InsertData(
                table: "Lookups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 97, "open-source capacitor" },
                    { 98, "redundant application" },
                    { 99, "back-end program" },
                    { 100, "virtual bandwidth" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lookups");
        }
    }
}
