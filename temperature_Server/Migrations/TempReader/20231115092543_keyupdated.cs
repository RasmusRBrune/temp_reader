using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace temperature_Server.Migrations.TempReader
{
    /// <inheritdoc />
    public partial class keyupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "TemperatureReaderDeviceKey",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureReaderDeviceKey_Key",
                table: "TemperatureReaderDeviceKey",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TemperatureReaderDeviceKey_Key",
                table: "TemperatureReaderDeviceKey");

            migrationBuilder.AlterColumn<string>(
                name: "Key",
                table: "TemperatureReaderDeviceKey",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
