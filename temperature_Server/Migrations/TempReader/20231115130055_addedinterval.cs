using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace temperature_Server.Migrations.TempReader
{
    /// <inheritdoc />
    public partial class addedinterval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IntervalInMinutes",
                table: "Devices",
                type: "int",
                nullable: false,
                defaultValue: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalInMinutes",
                table: "Devices");
        }
    }
}
