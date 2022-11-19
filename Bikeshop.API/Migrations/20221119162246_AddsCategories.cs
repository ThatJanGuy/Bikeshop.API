using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bikeshop.API.Migrations
{
    /// <inheritdoc />
    public partial class AddsCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("650f46a0-5d88-4e89-8d53-b59843fea698"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("9e0c7aba-d265-478c-8ad8-5adbfe41e762"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("184c54ce-1811-40d9-9f6f-8eeff98035ca"), null, null, "It might look very silly nowadays, but it sure does turn heads.", "Penny Farthing", null, null, "That iconic bike" },
                    { new Guid("aa655ffe-74e0-480f-bb3c-49bd7055620e"), null, null, "The number one choice for mountainous adventures.", "Aplha Explorer", null, null, "Mud has nothing on it" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"), "Metal goats", "Mountainbikes" },
                    { new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"), "The bikes you always wanted", "Specialty bikes" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("184c54ce-1811-40d9-9f6f-8eeff98035ca"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("aa655ffe-74e0-480f-bb3c-49bd7055620e"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Categories",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("650f46a0-5d88-4e89-8d53-b59843fea698"), null, null, null, "Mountainbike", null, null, null },
                    { new Guid("9e0c7aba-d265-478c-8ad8-5adbfe41e762"), null, null, null, "Penny Farthing", null, null, null }
                });
        }
    }
}
