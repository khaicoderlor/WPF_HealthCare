using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class V3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStep_ServiceStep_ServiceStepId",
                table: "OrderStep");

            migrationBuilder.DropIndex(
                name: "IX_OrderStep_ServiceStepId",
                table: "OrderStep");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStep_ServiceStepId",
                table: "OrderStep",
                column: "ServiceStepId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStep_ServiceStep_ServiceStepId",
                table: "OrderStep",
                column: "ServiceStepId",
                principalTable: "ServiceStep",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderStep_ServiceStep_ServiceStepId",
                table: "OrderStep");

            migrationBuilder.DropIndex(
                name: "IX_OrderStep_ServiceStepId",
                table: "OrderStep");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStep_ServiceStepId",
                table: "OrderStep",
                column: "ServiceStepId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderStep_ServiceStep_ServiceStepId",
                table: "OrderStep",
                column: "ServiceStepId",
                principalTable: "ServiceStep",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
