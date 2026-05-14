using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEAT_SECTOR_SectorId",
                table: "SEAT");

            migrationBuilder.InsertData(
                table: "EVENT",
                columns: new[] { "Id", "Description", "EventDate", "Name", "Status", "Url1", "Url2", "Venue" },
                values: new object[] { 1, "Action", new DateTime(2026, 7, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Movie", "Available", "https://www.elcineenlasombra.com/wp-content/uploads/2014/12/the-hobbit-the-desolation-of-smaug-22982-2880x1800-copia.jpg", "https://beam-images.warnermediacdn.com/BEAM_LWM_DELIVERABLES/6ba42b80-1619-4ed4-b250-0f0718fd3141/f6b2b5af2d4217fca21c52e6b286f67bd78c2d79.jpg?host=wbd-images.prod-vod.h264.io&partner=beamcom&w=500", "The Hobbit" });

            migrationBuilder.InsertData(
                table: "SECTOR",
                columns: new[] { "Id", "Capacity", "Cols", "EventId", "GridX", "GridY", "Name", "Price", "Rows" },
                values: new object[,]
                {
                    { 1, 100, 5, 1, 0, 0, "VIP", 800m, 5 },
                    { 2, 1000, 5, 1, 0, 0, "NORMAL", 450m, 5 }
                });

            migrationBuilder.InsertData(
                table: "SEAT",
                columns: new[] { "Id", "RowIdentifier", "SeatNumber", "SectorId", "Status" },
                values: new object[,]
                {
                    { new Guid("05bd49be-0a08-4091-96f2-390e052fb995"), "A", 5, 2, 0 },
                    { new Guid("063b9ffe-52fa-4acc-bbf7-a2970a4e6fa8"), "E", 1, 2, 0 },
                    { new Guid("07cbd32c-de75-4c9e-82bd-c5d0288deb09"), "C", 1, 2, 0 },
                    { new Guid("117b330b-0468-45a0-8596-a010c2a8d212"), "E", 2, 1, 0 },
                    { new Guid("12f1e066-489b-4be0-81d4-61d26edc7691"), "D", 2, 2, 0 },
                    { new Guid("1530e3c3-3771-412a-a410-867f5645e937"), "C", 4, 2, 0 },
                    { new Guid("168c6bc5-4075-463e-8c98-e2c2f5f3a3ee"), "B", 4, 1, 0 },
                    { new Guid("2baa396f-293c-41aa-8b65-b9fab106fb5e"), "B", 1, 1, 0 },
                    { new Guid("324cf69a-df84-491a-9190-8b0aecfd7928"), "E", 3, 2, 0 },
                    { new Guid("35a375a6-ab29-4479-86be-65e8caaaa641"), "E", 1, 1, 0 },
                    { new Guid("37c9859f-4c14-4ef2-93e9-4ded43f4e29b"), "C", 3, 1, 0 },
                    { new Guid("3c74e33e-0a33-47c1-bd15-547b63d26fa7"), "B", 1, 2, 0 },
                    { new Guid("3f2dd947-7228-436c-9e81-f8f1d2b7b3b7"), "B", 3, 2, 0 },
                    { new Guid("45157000-ac9a-419a-9eae-b211cff6f5ef"), "D", 5, 2, 0 },
                    { new Guid("46fdc975-7e20-44cd-8003-cac8f6244c70"), "A", 4, 2, 0 },
                    { new Guid("5b830693-2649-4831-ab12-8606d119bd88"), "A", 4, 1, 0 },
                    { new Guid("5db1774e-11bd-49d7-8189-cccca5db892b"), "B", 3, 1, 0 },
                    { new Guid("5f0a8fe4-e4f2-4817-a03f-06932b573257"), "E", 5, 2, 0 },
                    { new Guid("646f5236-1dca-4d81-8733-8f731e88fc18"), "D", 5, 1, 0 },
                    { new Guid("692ff031-2ec7-4258-8a72-15ce87ebc37d"), "D", 4, 2, 0 },
                    { new Guid("69af3ca1-35af-4515-a91b-86efe1a92ce2"), "A", 3, 1, 0 },
                    { new Guid("69d2f9a6-8561-4a4f-8abd-7744e31fce6d"), "D", 2, 1, 0 },
                    { new Guid("6a3749bc-659d-4ae6-806d-74f0ff36e262"), "E", 2, 2, 0 },
                    { new Guid("6e9b20ac-2332-4601-a66b-f736b1d248d2"), "C", 5, 2, 0 },
                    { new Guid("7011c175-a118-422d-bba7-888ecd458256"), "C", 1, 1, 0 },
                    { new Guid("780b221f-2a3a-43ff-99e2-27f09cfcc93b"), "B", 2, 1, 0 },
                    { new Guid("8292af1e-f408-49a6-b416-35d55eef8baa"), "C", 2, 2, 0 },
                    { new Guid("8df2c631-96fb-4c3c-b2c1-5c3d24671a34"), "B", 5, 2, 0 },
                    { new Guid("8e2738ca-8e9b-4823-ab25-1ebb1291d800"), "C", 3, 2, 0 },
                    { new Guid("8f1ac601-a31a-46ef-940c-43ed55b243a9"), "D", 3, 1, 0 },
                    { new Guid("946510b5-6dd8-4c7e-a0d9-dc283605e606"), "A", 1, 2, 0 },
                    { new Guid("96fa876b-50cd-4953-b05b-5c691de9ceba"), "B", 4, 2, 0 },
                    { new Guid("98564824-bc6b-4ce0-abca-8fd2ca7306be"), "E", 5, 1, 0 },
                    { new Guid("9e7e9603-942a-47a9-a664-154d33aa2120"), "B", 5, 1, 0 },
                    { new Guid("a8b1c8d9-4c3d-4b4e-b626-23d4bc6d457d"), "E", 4, 1, 0 },
                    { new Guid("b1c844f1-d2a7-4999-9fc6-1b0b34d2d1b1"), "E", 4, 2, 0 },
                    { new Guid("b886486d-c61d-4eb0-b01b-42a6b30d19f6"), "B", 2, 2, 0 },
                    { new Guid("bbebe8ac-dcbd-4a12-90cd-5956092f657c"), "D", 1, 1, 0 },
                    { new Guid("bc56ec5f-7241-473c-b186-70a35329f7d6"), "C", 2, 1, 0 },
                    { new Guid("bf2d9006-7909-4cd1-93e0-54f8c9c0c138"), "A", 1, 1, 0 },
                    { new Guid("c20741f2-818e-42a1-a0c5-bc602155b8b4"), "E", 3, 1, 0 },
                    { new Guid("cc5f741c-e4e8-44e3-82c2-18fabd7c4311"), "D", 1, 2, 0 },
                    { new Guid("cf64f73f-3e95-4eb5-93e8-793e28c927c1"), "A", 2, 1, 0 },
                    { new Guid("db6e0dc6-30eb-4f9d-879c-e6e93c41a1d0"), "C", 4, 1, 0 },
                    { new Guid("e5ca8a53-8515-488c-822e-a6793894ad67"), "C", 5, 1, 0 },
                    { new Guid("ee18d8c8-3d4a-4b30-8390-92ed09dd26a1"), "A", 2, 2, 0 },
                    { new Guid("f9c74f85-0337-4da6-8814-15eca3d9013c"), "D", 4, 1, 0 },
                    { new Guid("fb0a782a-6f2a-42b0-81d4-ffb085280cbc"), "A", 5, 1, 0 },
                    { new Guid("fb8573d3-efe8-43df-83ec-411e4f2c1d9c"), "A", 3, 2, 0 },
                    { new Guid("fc357336-ad6c-4d87-9f06-a3c7fa0456b0"), "D", 3, 2, 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SEAT_SECTOR_SectorId",
                table: "SEAT",
                column: "SectorId",
                principalTable: "SECTOR",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SEAT_SECTOR_SectorId",
                table: "SEAT");

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("05bd49be-0a08-4091-96f2-390e052fb995"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("063b9ffe-52fa-4acc-bbf7-a2970a4e6fa8"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("07cbd32c-de75-4c9e-82bd-c5d0288deb09"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("117b330b-0468-45a0-8596-a010c2a8d212"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("12f1e066-489b-4be0-81d4-61d26edc7691"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("1530e3c3-3771-412a-a410-867f5645e937"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("168c6bc5-4075-463e-8c98-e2c2f5f3a3ee"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("2baa396f-293c-41aa-8b65-b9fab106fb5e"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("324cf69a-df84-491a-9190-8b0aecfd7928"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("35a375a6-ab29-4479-86be-65e8caaaa641"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("37c9859f-4c14-4ef2-93e9-4ded43f4e29b"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("3c74e33e-0a33-47c1-bd15-547b63d26fa7"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("3f2dd947-7228-436c-9e81-f8f1d2b7b3b7"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("45157000-ac9a-419a-9eae-b211cff6f5ef"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("46fdc975-7e20-44cd-8003-cac8f6244c70"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("5b830693-2649-4831-ab12-8606d119bd88"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("5db1774e-11bd-49d7-8189-cccca5db892b"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("5f0a8fe4-e4f2-4817-a03f-06932b573257"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("646f5236-1dca-4d81-8733-8f731e88fc18"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("692ff031-2ec7-4258-8a72-15ce87ebc37d"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("69af3ca1-35af-4515-a91b-86efe1a92ce2"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("69d2f9a6-8561-4a4f-8abd-7744e31fce6d"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("6a3749bc-659d-4ae6-806d-74f0ff36e262"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("6e9b20ac-2332-4601-a66b-f736b1d248d2"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("7011c175-a118-422d-bba7-888ecd458256"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("780b221f-2a3a-43ff-99e2-27f09cfcc93b"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8292af1e-f408-49a6-b416-35d55eef8baa"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8df2c631-96fb-4c3c-b2c1-5c3d24671a34"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8e2738ca-8e9b-4823-ab25-1ebb1291d800"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("8f1ac601-a31a-46ef-940c-43ed55b243a9"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("946510b5-6dd8-4c7e-a0d9-dc283605e606"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("96fa876b-50cd-4953-b05b-5c691de9ceba"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("98564824-bc6b-4ce0-abca-8fd2ca7306be"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("9e7e9603-942a-47a9-a664-154d33aa2120"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("a8b1c8d9-4c3d-4b4e-b626-23d4bc6d457d"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("b1c844f1-d2a7-4999-9fc6-1b0b34d2d1b1"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("b886486d-c61d-4eb0-b01b-42a6b30d19f6"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("bbebe8ac-dcbd-4a12-90cd-5956092f657c"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("bc56ec5f-7241-473c-b186-70a35329f7d6"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("bf2d9006-7909-4cd1-93e0-54f8c9c0c138"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("c20741f2-818e-42a1-a0c5-bc602155b8b4"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("cc5f741c-e4e8-44e3-82c2-18fabd7c4311"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("cf64f73f-3e95-4eb5-93e8-793e28c927c1"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("db6e0dc6-30eb-4f9d-879c-e6e93c41a1d0"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("e5ca8a53-8515-488c-822e-a6793894ad67"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("ee18d8c8-3d4a-4b30-8390-92ed09dd26a1"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("f9c74f85-0337-4da6-8814-15eca3d9013c"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("fb0a782a-6f2a-42b0-81d4-ffb085280cbc"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("fb8573d3-efe8-43df-83ec-411e4f2c1d9c"));

            migrationBuilder.DeleteData(
                table: "SEAT",
                keyColumn: "Id",
                keyValue: new Guid("fc357336-ad6c-4d87-9f06-a3c7fa0456b0"));

            migrationBuilder.DeleteData(
                table: "SECTOR",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SECTOR",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "EVENT",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_SEAT_SECTOR_SectorId",
                table: "SEAT",
                column: "SectorId",
                principalTable: "SECTOR",
                principalColumn: "Id");
        }
    }
}
