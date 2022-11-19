using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bikeshop.API.Migrations
{
    /// <inheritdoc />
    public partial class AddsCategoriesToSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("184c54ce-1811-40d9-9f6f-8eeff98035ca"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("aa655ffe-74e0-480f-bb3c-49bd7055620e"));

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("01d80c1c-25d1-4564-af58-a01663937f22"), new Guid("e41b9f14-f19f-4dca-8462-bf33d8ce0bb2"), null, "The number one choice for mountainous adventures.", "Aplha Explorer", null, null, "Mud has nothing on it" },
                    { new Guid("1e3ce34f-5fb8-4991-828c-f31ddf3a4792"), new Guid("ed4e9bee-5810-40b8-b906-91c92f134ece"), null, "It might look very silly nowadays, but it sure does turn heads.", "Penny Farthing", null, null, "That iconic bike" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("01d80c1c-25d1-4564-af58-a01663937f22"));

            migrationBuilder.DeleteData(
                table: "Bikes",
                keyColumn: "Id",
                keyValue: new Guid("1e3ce34f-5fb8-4991-828c-f31ddf3a4792"));

            migrationBuilder.InsertData(
                table: "Bikes",
                columns: new[] { "Id", "CategoryId", "Colour", "FullDescription", "Name", "PictureUrl", "Price", "ShortDescription" },
                values: new object[,]
                {
                    { new Guid("184c54ce-1811-40d9-9f6f-8eeff98035ca"), null, null, "It might look very silly nowadays, but it sure does turn heads.", "Penny Farthing", null, null, "That iconic bike" },
                    { new Guid("aa655ffe-74e0-480f-bb3c-49bd7055620e"), null, null, "The number one choice for mountainous adventures.", "Aplha Explorer", null, null, "Mud has nothing on it" }
                });
        }
    }
}
