using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrrigationSystemServer.Migrations
{
    public partial class AddedPinMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorCount",
                table: "DeviceTypes");

            migrationBuilder.DropColumn(
                name: "ValveCount",
                table: "DeviceTypes");

            migrationBuilder.AddColumn<string>(
                name: "SensorPinMap",
                table: "DeviceTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValvePinMap",
                table: "DeviceTypes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SensorPinMap",
                table: "DeviceTypes");

            migrationBuilder.DropColumn(
                name: "ValvePinMap",
                table: "DeviceTypes");

            migrationBuilder.AddColumn<int>(
                name: "SensorCount",
                table: "DeviceTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ValveCount",
                table: "DeviceTypes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
