using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace temperature_Server.Migrations.TempReader
{
    /// <inheritdoc />
    public partial class newEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices");

            migrationBuilder.CreateTable(
                name: "TemperatureReaderDeviceKey",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureReaderDeviceKey", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemperatureReaderDeviceKey_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TemperatureReaderDeviceKey_DeviceId",
                table: "TemperatureReaderDeviceKey",
                column: "DeviceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices");

            migrationBuilder.DropTable(
                name: "TemperatureReaderDeviceKey");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id");
        }
    }
}
