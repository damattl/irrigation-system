using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrrigationSystemServer.Migrations
{
    public partial class ChangedLongToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "SensorReading",
                table: "MoistureSensorData",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SensorReading",
                table: "MoistureSensorData",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "REAL");
        }
    }
}
