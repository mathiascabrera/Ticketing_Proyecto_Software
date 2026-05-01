using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEventImageUrlAndPreCharge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("0364f6e4-a955-4bd6-8a20-a4e3c07d1474"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("213b92b8-7d5f-44de-9995-2ddcae0e68d0"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("2ff47059-c0e3-4da1-82af-1391202d44e5"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("340bb653-e076-469e-b536-a0d7d336594e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("3569d2e4-be26-4c37-a706-720f4eb79986"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("372f8c58-301c-4217-9774-6cddd8a04f19"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("37e4c2c2-68c7-4035-b535-7a81b360f2bc"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("3ae07be2-b5ce-46b8-bd21-c3c3a7bb3b99"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("4c59a4d8-e594-4ab5-a710-c4008a2a8467"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("5640aaec-a70c-431a-b7cd-48bd7b18b41e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("5c0fddae-e0cc-46c2-917b-7f023f6ddff7"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("66b02d09-55e0-4a49-bcda-63d3f756b4aa"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("67755d7e-3e5f-4635-8e9d-6a677218119d"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("6dcb80d4-29dd-46e6-b376-6598667d2d8c"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("744b81a1-5cee-4bc9-92b3-af0b65e0a3f4"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("76613e3a-68b1-4360-ae8a-36f085a17ec5"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("797f299c-933e-4047-b417-a5fa147c7c7e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("7c4e21d6-1d2f-4055-aadc-3277157ce312"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("7f4172a0-6bf4-416e-b4d5-8b0ffe262728"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8bcc6e1e-4811-445d-816b-05a6669b38e8"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8eb9fddf-1bf0-4453-acad-7dc5074d08b1"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("925c385f-f8f6-4e04-9451-36aa2b209c6c"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("9347e0d4-ae55-4348-b580-fdc6c24c2fac"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("94194a7f-80d6-40e3-a24d-0914c487fd4a"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("94a1f709-dfa7-4d25-9ff6-c4d390a2960e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("968fe037-bead-4181-b738-33eb8ba3c63a"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("99879152-9eca-4234-9a5b-369d76cb84d9"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("9ec49cea-81f7-4b58-831b-17278f1b74e7"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("a0387936-93f8-4802-9443-f49318b564eb"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("a0efcc6a-765f-454f-83ed-d5c610280df8"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("a72e1a0d-d514-4b02-967a-3a63d8d79835"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("b04682b1-76fa-48b4-8eb0-f4fc206f3fe5"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("b35ad7a5-2ad3-4ae5-86e6-67b533a91edc"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ba6a3e71-10b0-4d47-807d-94e6edfacbff"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("c54cc009-dbfa-40fd-ab2b-501f61076dbf"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("c91fadbb-0999-4595-a873-c30f2db91f9e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ccb6810b-1265-4274-a194-73fabac39f26"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("d682a818-e463-4896-9793-7d804c73f0d5"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("db198071-9843-4baf-b24c-9e653d5c90b0"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e2f05022-8ad8-4de3-8013-aaf7611ebfd4"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e36400ef-cd88-4347-86be-82e77d1d7972"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e42afc8c-5bfb-49f1-b07c-8869ae976b99"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e42dcf1a-8e90-47a0-a9eb-cb76fd0c1bfd"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e58908af-d0b2-408e-b6da-9036b7a8fac9"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ea41fd34-e108-4da0-9f42-bb918345e2b0"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ecd06841-3a4f-4335-9e29-647a5b8cf7c8"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ed2ff2ad-f583-40fb-98ff-8806aec0c793"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("f39cbb83-8fb6-4197-8447-2934e863cb56"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("f5ed9be5-df8a-4c4f-8cf8-27360d9e38a3"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("fa5a958a-71e1-4b8b-bca2-db926388c6d0"));

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SECTOR",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldDefaultValue: 0.00m);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "EVENT",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Name", "Venue" },
                values: new object[] { "", "concierto de rock", "la renga" });

            migrationBuilder.InsertData(
                table: "SEAT",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111001"), "A", 1, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111002"), "A", 2, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111003"), "A", 3, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111004"), "A", 4, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111005"), "A", 5, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111006"), "A", 6, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111007"), "A", 7, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111008"), "A", 8, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111009"), "A", 9, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111010"), "A", 10, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111011"), "B", 1, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111012"), "B", 2, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111013"), "B", 3, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111014"), "B", 4, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111015"), "B", 5, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111016"), "B", 6, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111017"), "B", 7, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111018"), "B", 8, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111019"), "B", 9, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111020"), "B", 10, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111021"), "C", 1, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111022"), "C", 2, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111023"), "C", 3, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111024"), "C", 4, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111025"), "C", 5, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111026"), "C", 6, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111027"), "C", 7, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111028"), "C", 8, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111029"), "C", 9, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111030"), "C", 10, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111031"), "D", 1, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111032"), "D", 2, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111033"), "D", 3, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111034"), "D", 4, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111035"), "D", 5, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111036"), "D", 6, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111037"), "D", 7, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111038"), "D", 8, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111039"), "D", 9, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111040"), "D", 10, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111041"), "E", 1, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111042"), "E", 2, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111043"), "E", 3, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111044"), "E", 4, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111045"), "E", 5, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111046"), "E", 6, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111047"), "E", 7, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111048"), "E", 8, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111049"), "E", 9, 1, "Available", 0 },
                    { new Guid("11111111-1111-1111-1111-111111111050"), "E", 10, 1, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222001"), "A", 1, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222002"), "A", 2, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222003"), "A", 3, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222004"), "A", 4, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222005"), "A", 5, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222006"), "A", 6, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222007"), "A", 7, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222008"), "A", 8, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222009"), "A", 9, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222010"), "A", 10, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222011"), "B", 1, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222012"), "B", 2, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222013"), "B", 3, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222014"), "B", 4, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222015"), "B", 5, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222016"), "B", 6, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222017"), "B", 7, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222018"), "B", 8, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222019"), "B", 9, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222020"), "B", 10, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222021"), "C", 1, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222022"), "C", 2, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222023"), "C", 3, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222024"), "C", 4, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222025"), "C", 5, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222026"), "C", 6, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222027"), "C", 7, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222028"), "C", 8, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222029"), "C", 9, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222030"), "C", 10, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222031"), "D", 1, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222032"), "D", 2, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222033"), "D", 3, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222034"), "D", 4, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222035"), "D", 5, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222036"), "D", 6, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222037"), "D", 7, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222038"), "D", 8, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222039"), "D", 9, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222040"), "D", 10, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222041"), "E", 1, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222042"), "E", 2, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222043"), "E", 3, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222044"), "E", 4, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222045"), "E", 5, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222046"), "E", 6, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222047"), "E", 7, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222048"), "E", 8, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222049"), "E", 9, 2, "Available", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222050"), "E", 10, 2, "Available", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111001"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111002"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111003"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111004"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111005"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111006"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111007"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111008"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111009"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111010"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111011"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111012"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111013"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111014"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111015"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111016"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111017"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111018"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111019"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111020"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111021"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111022"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111023"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111024"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111025"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111026"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111027"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111028"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111029"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111030"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111031"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111032"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111033"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111034"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111035"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111036"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111037"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111038"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111039"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111040"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111041"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111042"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111043"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111044"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111045"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111046"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111047"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111048"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111049"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111050"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222001"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222002"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222003"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222004"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222005"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222006"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222007"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222008"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222009"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222010"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222011"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222012"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222013"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222014"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222015"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222016"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222017"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222018"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222019"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222020"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222021"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222022"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222023"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222024"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222025"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222026"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222027"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222028"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222029"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222030"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222031"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222032"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222033"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222034"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222035"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222036"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222037"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222038"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222039"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222040"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222041"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222042"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222043"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222044"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222045"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222046"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222047"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222048"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222049"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222050"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "EVENT");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "SECTOR",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0.00m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.UpdateData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Venue" },
                values: new object[] { "Movie", "The Hobbit" });

            migrationBuilder.InsertData(
                table: "SEAT",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status", "Version" },
                values: new object[,]
                {
                    { new Guid("0364f6e4-a955-4bd6-8a20-a4e3c07d1474"), "B", 3, 2, "Available", 0 },
                    { new Guid("213b92b8-7d5f-44de-9995-2ddcae0e68d0"), "C", 5, 1, "Available", 0 },
                    { new Guid("2ff47059-c0e3-4da1-82af-1391202d44e5"), "D", 4, 2, "Available", 0 },
                    { new Guid("340bb653-e076-469e-b536-a0d7d336594e"), "D", 2, 1, "Available", 0 },
                    { new Guid("3569d2e4-be26-4c37-a706-720f4eb79986"), "B", 1, 2, "Available", 0 },
                    { new Guid("372f8c58-301c-4217-9774-6cddd8a04f19"), "E", 3, 1, "Available", 0 },
                    { new Guid("37e4c2c2-68c7-4035-b535-7a81b360f2bc"), "B", 4, 2, "Available", 0 },
                    { new Guid("3ae07be2-b5ce-46b8-bd21-c3c3a7bb3b99"), "C", 4, 2, "Available", 0 },
                    { new Guid("4c59a4d8-e594-4ab5-a710-c4008a2a8467"), "E", 1, 1, "Available", 0 },
                    { new Guid("5640aaec-a70c-431a-b7cd-48bd7b18b41e"), "E", 4, 2, "Available", 0 },
                    { new Guid("5c0fddae-e0cc-46c2-917b-7f023f6ddff7"), "A", 2, 1, "Available", 0 },
                    { new Guid("66b02d09-55e0-4a49-bcda-63d3f756b4aa"), "D", 4, 1, "Available", 0 },
                    { new Guid("67755d7e-3e5f-4635-8e9d-6a677218119d"), "D", 1, 2, "Available", 0 },
                    { new Guid("6dcb80d4-29dd-46e6-b376-6598667d2d8c"), "B", 4, 1, "Available", 0 },
                    { new Guid("744b81a1-5cee-4bc9-92b3-af0b65e0a3f4"), "E", 1, 2, "Available", 0 },
                    { new Guid("76613e3a-68b1-4360-ae8a-36f085a17ec5"), "B", 5, 1, "Available", 0 },
                    { new Guid("797f299c-933e-4047-b417-a5fa147c7c7e"), "D", 3, 2, "Available", 0 },
                    { new Guid("7c4e21d6-1d2f-4055-aadc-3277157ce312"), "A", 3, 1, "Available", 0 },
                    { new Guid("7f4172a0-6bf4-416e-b4d5-8b0ffe262728"), "D", 3, 1, "Available", 0 },
                    { new Guid("8bcc6e1e-4811-445d-816b-05a6669b38e8"), "E", 2, 1, "Available", 0 },
                    { new Guid("8eb9fddf-1bf0-4453-acad-7dc5074d08b1"), "C", 2, 1, "Available", 0 },
                    { new Guid("925c385f-f8f6-4e04-9451-36aa2b209c6c"), "B", 2, 1, "Available", 0 },
                    { new Guid("9347e0d4-ae55-4348-b580-fdc6c24c2fac"), "A", 5, 2, "Available", 0 },
                    { new Guid("94194a7f-80d6-40e3-a24d-0914c487fd4a"), "B", 2, 2, "Available", 0 },
                    { new Guid("94a1f709-dfa7-4d25-9ff6-c4d390a2960e"), "A", 5, 1, "Available", 0 },
                    { new Guid("968fe037-bead-4181-b738-33eb8ba3c63a"), "C", 4, 1, "Available", 0 },
                    { new Guid("99879152-9eca-4234-9a5b-369d76cb84d9"), "E", 5, 1, "Available", 0 },
                    { new Guid("9ec49cea-81f7-4b58-831b-17278f1b74e7"), "E", 5, 2, "Available", 0 },
                    { new Guid("a0387936-93f8-4802-9443-f49318b564eb"), "B", 3, 1, "Available", 0 },
                    { new Guid("a0efcc6a-765f-454f-83ed-d5c610280df8"), "A", 1, 2, "Available", 0 },
                    { new Guid("a72e1a0d-d514-4b02-967a-3a63d8d79835"), "C", 1, 2, "Available", 0 },
                    { new Guid("b04682b1-76fa-48b4-8eb0-f4fc206f3fe5"), "A", 4, 1, "Available", 0 },
                    { new Guid("b35ad7a5-2ad3-4ae5-86e6-67b533a91edc"), "B", 1, 1, "Available", 0 },
                    { new Guid("ba6a3e71-10b0-4d47-807d-94e6edfacbff"), "A", 1, 1, "Available", 0 },
                    { new Guid("c54cc009-dbfa-40fd-ab2b-501f61076dbf"), "B", 5, 2, "Available", 0 },
                    { new Guid("c91fadbb-0999-4595-a873-c30f2db91f9e"), "D", 5, 1, "Available", 0 },
                    { new Guid("ccb6810b-1265-4274-a194-73fabac39f26"), "E", 4, 1, "Available", 0 },
                    { new Guid("d682a818-e463-4896-9793-7d804c73f0d5"), "D", 1, 1, "Available", 0 },
                    { new Guid("db198071-9843-4baf-b24c-9e653d5c90b0"), "C", 5, 2, "Available", 0 },
                    { new Guid("e2f05022-8ad8-4de3-8013-aaf7611ebfd4"), "A", 3, 2, "Available", 0 },
                    { new Guid("e36400ef-cd88-4347-86be-82e77d1d7972"), "E", 2, 2, "Available", 0 },
                    { new Guid("e42afc8c-5bfb-49f1-b07c-8869ae976b99"), "C", 2, 2, "Available", 0 },
                    { new Guid("e42dcf1a-8e90-47a0-a9eb-cb76fd0c1bfd"), "D", 2, 2, "Available", 0 },
                    { new Guid("e58908af-d0b2-408e-b6da-9036b7a8fac9"), "E", 3, 2, "Available", 0 },
                    { new Guid("ea41fd34-e108-4da0-9f42-bb918345e2b0"), "A", 2, 2, "Available", 0 },
                    { new Guid("ecd06841-3a4f-4335-9e29-647a5b8cf7c8"), "D", 5, 2, "Available", 0 },
                    { new Guid("ed2ff2ad-f583-40fb-98ff-8806aec0c793"), "C", 1, 1, "Available", 0 },
                    { new Guid("f39cbb83-8fb6-4197-8447-2934e863cb56"), "C", 3, 1, "Available", 0 },
                    { new Guid("f5ed9be5-df8a-4c4f-8cf8-27360d9e38a3"), "C", 3, 2, "Available", 0 },
                    { new Guid("fa5a958a-71e1-4b8b-bca2-db926388c6d0"), "A", 4, 2, "Available", 0 }
                });
        }
    }
}
