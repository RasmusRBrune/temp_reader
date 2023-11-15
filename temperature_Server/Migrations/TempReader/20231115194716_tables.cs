using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace temperature_Server.Migrations.TempReader
{
    /// <inheritdoc />
    public partial class tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_DeviceTimeLog_Devices_DeviceId",
                table: "DeviceTimeLog");

            migrationBuilder.DropForeignKey(
                name: "FK_TemperatureReaderDeviceKey_Devices_DeviceId",
                table: "TemperatureReaderDeviceKey");

            migrationBuilder.DropForeignKey(
                name: "FK_TempsReading_Devices_DeviceId",
                table: "TempsReading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TempsReading",
                table: "TempsReading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TemperatureReaderDeviceKey",
                table: "TemperatureReaderDeviceKey");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceTimeLog",
                table: "DeviceTimeLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Account",
                table: "Account");

            migrationBuilder.RenameTable(
                name: "TempsReading",
                newName: "Readings");

            migrationBuilder.RenameTable(
                name: "TemperatureReaderDeviceKey",
                newName: "Keys");

            migrationBuilder.RenameTable(
                name: "DeviceTimeLog",
                newName: "TimeLog");

            migrationBuilder.RenameTable(
                name: "Account",
                newName: "Accounts");

            migrationBuilder.RenameIndex(
                name: "IX_TempsReading_DeviceId",
                table: "Readings",
                newName: "IX_Readings_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_TemperatureReaderDeviceKey_Key",
                table: "Keys",
                newName: "IX_Keys_Key");

            migrationBuilder.RenameIndex(
                name: "IX_TemperatureReaderDeviceKey_DeviceId",
                table: "Keys",
                newName: "IX_Keys_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceTimeLog_DeviceId",
                table: "TimeLog",
                newName: "IX_TimeLog_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Account_UserId",
                table: "Accounts",
                newName: "IX_Accounts_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Readings",
                table: "Readings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Keys",
                table: "Keys",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeLog",
                table: "TimeLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Accounts_AccountId",
                table: "Devices",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Devices_DeviceId",
                table: "Keys",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Readings_Devices_DeviceId",
                table: "Readings",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeLog_Devices_DeviceId",
                table: "TimeLog",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Accounts_AccountId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Devices_DeviceId",
                table: "Keys");

            migrationBuilder.DropForeignKey(
                name: "FK_Readings_Devices_DeviceId",
                table: "Readings");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeLog_Devices_DeviceId",
                table: "TimeLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeLog",
                table: "TimeLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Readings",
                table: "Readings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Keys",
                table: "Keys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "TimeLog",
                newName: "DeviceTimeLog");

            migrationBuilder.RenameTable(
                name: "Readings",
                newName: "TempsReading");

            migrationBuilder.RenameTable(
                name: "Keys",
                newName: "TemperatureReaderDeviceKey");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "Account");

            migrationBuilder.RenameIndex(
                name: "IX_TimeLog_DeviceId",
                table: "DeviceTimeLog",
                newName: "IX_DeviceTimeLog_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Readings_DeviceId",
                table: "TempsReading",
                newName: "IX_TempsReading_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_Key",
                table: "TemperatureReaderDeviceKey",
                newName: "IX_TemperatureReaderDeviceKey_Key");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_DeviceId",
                table: "TemperatureReaderDeviceKey",
                newName: "IX_TemperatureReaderDeviceKey_DeviceId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_UserId",
                table: "Account",
                newName: "IX_Account_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceTimeLog",
                table: "DeviceTimeLog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempsReading",
                table: "TempsReading",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TemperatureReaderDeviceKey",
                table: "TemperatureReaderDeviceKey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Account",
                table: "Account",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Account_AccountId",
                table: "Devices",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceTimeLog_Devices_DeviceId",
                table: "DeviceTimeLog",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TemperatureReaderDeviceKey_Devices_DeviceId",
                table: "TemperatureReaderDeviceKey",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TempsReading_Devices_DeviceId",
                table: "TempsReading",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
