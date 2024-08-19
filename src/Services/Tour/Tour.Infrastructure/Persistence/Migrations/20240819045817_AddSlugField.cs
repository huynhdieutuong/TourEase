using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tour.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSlugField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "TourJob",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TourDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Destination",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Destination",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TourJob_Slug",
                table: "TourJob",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destination_Slug",
                table: "Destination",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TourJob_Slug",
                table: "TourJob");

            migrationBuilder.DropIndex(
                name: "IX_Destination_Slug",
                table: "Destination");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "TourJob");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Destination");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Destination");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "TourDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
