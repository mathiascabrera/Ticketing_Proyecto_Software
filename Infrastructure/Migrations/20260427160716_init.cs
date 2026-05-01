using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EVENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVENT", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SECTOR",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0.00m),
                    Capacity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SECTOR", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SECTOR_EVENT_EventId",
                        column: x => x.EventId,
                        principalTable: "EVENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AUDIT_LOG",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EntityId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_LOG", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AUDIT_LOG_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SEAT",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SectorId = table.Column<int>(type: "int", nullable: false),
                    RowIdentifier = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEAT", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SEAT_SECTOR_SectorId",
                        column: x => x.SectorId,
                        principalTable: "SECTOR",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RESERVATION",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SeatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReservedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RESERVATION", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RESERVATION_SEAT_SeatId",
                        column: x => x.SeatId,
                        principalTable: "SEAT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RESERVATION_USER_UserId",
                        column: x => x.UserId,
                        principalTable: "USER",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "EVENT",
                columns: new[] { "Id", "EventDate", "Name", "Status", "Venue" },
                values: new object[] { 1, new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie", "Available", "The Hobbit" });

            migrationBuilder.InsertData(
                table: "SECTOR",
                columns: new[] { "Id", "Capacity", "EventId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 100, 1, "VIP", 800m },
                    { 2, 1000, 1, "NORMAL", 450m }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_AUDIT_LOG_UserId",
                table: "AUDIT_LOG",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_SeatId",
                table: "RESERVATION",
                column: "SeatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RESERVATION_UserId",
                table: "RESERVATION",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SEAT_SectorId",
                table: "SEAT",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_SECTOR_EventId",
                table: "SECTOR",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDIT_LOG");

            migrationBuilder.DropTable(
                name: "RESERVATION");

            migrationBuilder.DropTable(
                name: "SEAT");

            migrationBuilder.DropTable(
                name: "USER");

            migrationBuilder.DropTable(
                name: "SECTOR");

            migrationBuilder.DropTable(
                name: "EVENT");
        }
    }
}
