using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductFeatures",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DislikeCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductFeatures",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DislikeCount",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("17faf120-51a9-4917-bd6a-e43d60b2e910"),
                column: "ConcurrencyStamp",
                value: "fd7f9e1a-16f3-46cf-9608-0d73381b4760");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("30144a78-fa5f-45d8-9dbc-eb595667add7"),
                column: "ConcurrencyStamp",
                value: "a4644f7c-dc1a-4f16-b292-01f0698615b5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("70d5392a-4f59-41c5-9dd0-1e026005f59c"),
                column: "ConcurrencyStamp",
                value: "38fc0de4-4315-4a78-8129-3e6ef878a574");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("19b1856d-44a9-401b-b188-563b8b48ee21"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf57c70b-a64f-4eb9-9b97-a10454d9d510", "AQAAAAIAAYagAAAAECFq5VLHomFYgd/pMIsZLDztROvK7AXtQU/Xc8C/doHmT2j2RrTV7FOO48YEr3KWLw==", "6b1f33fe-3a7c-4ee6-a017-f70b02af9164" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("361936ea-8c5d-410e-8a5a-7f3f5c302d87"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3f62073-ad20-470e-a960-811ffc305ad5", "AQAAAAIAAYagAAAAEGj6/kx9Tx3maOMcwJHfLqkx8ecr22x0XWAU7wlJopzzbmJg/jQhZDDPmRYNwhjn9A==", "5e90cd3e-3bc2-45dd-a5cd-265c15440698" });
        }
    }
}
