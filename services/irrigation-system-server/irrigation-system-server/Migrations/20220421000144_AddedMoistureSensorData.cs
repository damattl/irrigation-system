using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrrigationSystemServer.Migrations
{
    public partial class AddedMoistureSensorData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoistureSensorData",
                columns: table => new
                {
                    MoistureSensorDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MoistureSensorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SensorReading = table.Column<long>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoistureSensorData", x => x.MoistureSensorDataId);
                    table.ForeignKey(
                        name: "FK_MoistureSensorData_MoistureSensors_MoistureSensorId",
                        column: x => x.MoistureSensorId,
                        principalTable: "MoistureSensors",
                        principalColumn: "MoistureSensorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoistureSensorData_MoistureSensorId",
                table: "MoistureSensorData",
                column: "MoistureSensorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoistureSensorData");
        }
    }
}
