using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Delete_Cascade_TourDetailDestination : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourDetailDestination_Destination_DestinationId",
                table: "TourDetailDestination");

            migrationBuilder.DropForeignKey(
                name: "FK_TourDetailDestination_TourDetail_TourDetailId",
                table: "TourDetailDestination");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourDetailId",
                table: "TourDetailDestination",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationId",
                table: "TourDetailDestination",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TourDetailDestination_Destination_DestinationId",
                table: "TourDetailDestination",
                column: "DestinationId",
                principalTable: "Destination",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TourDetailDestination_TourDetail_TourDetailId",
                table: "TourDetailDestination",
                column: "TourDetailId",
                principalTable: "TourDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourDetailDestination_Destination_DestinationId",
                table: "TourDetailDestination");

            migrationBuilder.DropForeignKey(
                name: "FK_TourDetailDestination_TourDetail_TourDetailId",
                table: "TourDetailDestination");

            migrationBuilder.AlterColumn<Guid>(
                name: "TourDetailId",
                table: "TourDetailDestination",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "DestinationId",
                table: "TourDetailDestination",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TourDetailDestination_Destination_DestinationId",
                table: "TourDetailDestination",
                column: "DestinationId",
                principalTable: "Destination",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TourDetailDestination_TourDetail_TourDetailId",
                table: "TourDetailDestination",
                column: "TourDetailId",
                principalTable: "TourDetail",
                principalColumn: "Id");
        }
    }
}
