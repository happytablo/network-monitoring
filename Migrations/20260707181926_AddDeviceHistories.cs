using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace a_webapi.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceHistories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatusHistory_Devices_DeviceId",
                table: "DeviceStatusHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceStatusHistory",
                table: "DeviceStatusHistory");

            migrationBuilder.RenameTable(
                name: "DeviceStatusHistory",
                newName: "DeviceStatusHistories");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceStatusHistory_DeviceId",
                table: "DeviceStatusHistories",
                newName: "IX_DeviceStatusHistories_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceStatusHistories",
                table: "DeviceStatusHistories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatusHistories_Devices_DeviceId",
                table: "DeviceStatusHistories",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeviceStatusHistories_Devices_DeviceId",
                table: "DeviceStatusHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeviceStatusHistories",
                table: "DeviceStatusHistories");

            migrationBuilder.RenameTable(
                name: "DeviceStatusHistories",
                newName: "DeviceStatusHistory");

            migrationBuilder.RenameIndex(
                name: "IX_DeviceStatusHistories_DeviceId",
                table: "DeviceStatusHistory",
                newName: "IX_DeviceStatusHistory_DeviceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeviceStatusHistory",
                table: "DeviceStatusHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeviceStatusHistory_Devices_DeviceId",
                table: "DeviceStatusHistory",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
