using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17faf120-51a9-4917-bd6a-e43d60b2e910"),
                column: "ConcurrencyStamp",
                value: "614deaba-049b-4ffb-865a-5e33c6baedfd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30144a78-fa5f-45d8-9dbc-eb595667add7"),
                column: "ConcurrencyStamp",
                value: "6db89337-be68-4382-9ea7-fb9d09acd7f4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70d5392a-4f59-41c5-9dd0-1e026005f59c"),
                column: "ConcurrencyStamp",
                value: "9b2bc38b-b31c-41d5-aea7-265161d8b75d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("19b1856d-44a9-401b-b188-563b8b48ee21"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76ffdd02-e0b5-4289-9217-f9e1de446610", "AQAAAAIAAYagAAAAEDOuwHQETJIkRxnThJbPoftuit7Y1LJbAt+mr5RGiCBiqFk0TWzb4cza/iB4PdOgkw==", "442a8556-6b57-4679-97d7-2688b18be58d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("361936ea-8c5d-410e-8a5a-7f3f5c302d87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5892c0fc-0b8f-4153-876a-af8995ed057e", "AQAAAAIAAYagAAAAEGr3MLKgwphrPTf++3/RFyKswgdtdB5EM36H4Ne+sOtvj9s8ax5x0ynRGfkhBqqy3Q==", "9fb97fbf-4877-4b13-a489-75bb2d1c3786" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductPrice = table.Column<float>(type: "real", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_Id",
                        column: x => x.Id,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17faf120-51a9-4917-bd6a-e43d60b2e910"),
                column: "ConcurrencyStamp",
                value: "dc48bfd7-6ba8-446a-a88c-ece339bc7984");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30144a78-fa5f-45d8-9dbc-eb595667add7"),
                column: "ConcurrencyStamp",
                value: "dad898fb-8f11-45a2-b67d-88431d329914");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70d5392a-4f59-41c5-9dd0-1e026005f59c"),
                column: "ConcurrencyStamp",
                value: "bcfb9db6-f5b7-40d9-8fcf-24ee7af1d8ba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("19b1856d-44a9-401b-b188-563b8b48ee21"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "041d1938-1c9d-4eca-b4ad-5b93b4fb1257", "AQAAAAIAAYagAAAAENTSNX+gcsdaGTszu7uUP8vpj7d5VN6+GPiQsNthSISQ0tXJQYlNA/FFBJt0Iz9AIQ==", "0255bf0e-c69c-44ca-afc3-66239b539e33" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("361936ea-8c5d-410e-8a5a-7f3f5c302d87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c90995b-ae51-42c4-9144-e9bd66516f16", "AQAAAAIAAYagAAAAEFv9PhiWldXtPFlnAmEfQ7b3LxMSYHrRikZZPltEd+48LKcDkcuR/usiomju3B+l8w==", "ce1d7ec7-4283-456d-8a94-82dff2a42553" });
        }
    }
}
