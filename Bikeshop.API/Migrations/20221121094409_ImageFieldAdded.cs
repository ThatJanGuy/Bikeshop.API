using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bikeshop.API.Migrations
{
    /// <inheritdoc />
    public partial class ImageFieldAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes");

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("01d80c1c-25d1-4564-af58-a01663937f22"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("1e3ce34f-5fb8-4991-828c-f31ddf3a4792"));

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Bikes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Bikes",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Image", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("3abb4b95-2497-4d35-adcb-18b2bdc80706"), new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"), null, "It might look very silly nowadays, but it sure does turn heads.", null, "Penny Farthing", null, null, "That iconic bike" },
                    { new Guid("83a8e0f2-eb9d-462f-adc3-c63588e0bea2"), new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"), null, "The number one choice for mountainous adventures.", null, "Aplha Explorer", null, null, "Mud has nothing on it" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes");

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("3abb4b95-2497-4d35-adcb-18b2bdc80706"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("83a8e0f2-eb9d-462f-adc3-c63588e0bea2"));

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Bikes");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Bikes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("01d80c1c-25d1-4564-af58-a01663937f22"), new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"), null, "The number one choice for mountainous adventures.", "Aplha Explorer", null, null, "Mud has nothing on it" },
                    { new Guid("1e3ce34f-5fb8-4991-828c-f31ddf3a4792"), new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"), null, "It might look very silly nowadays, but it sure does turn heads.", "Penny Farthing", null, null, "That iconic bike" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Bikes_Categories_CategoryId",
                table: "Bikes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
