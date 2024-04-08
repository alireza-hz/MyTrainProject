using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class createtbls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "MyCategoryID", "Createdate", "Isdelete", "ModifiedDate", "description" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9356), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9356), "گرافیک" },
                    { 2, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9358), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9358), "برنامه نویسی" },
                    { 3, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9360), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9360), "شبکه" },
                    { 4, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9361), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9361), "دیجیتال مارکتینگ" }
                });

            migrationBuilder.InsertData(
                table: "Levels",
                columns: new[] { "MYlevelID", "Description" },
                values: new object[,]
                {
                    { 1, "مبتدی" },
                    { 2, "پیشرفته" }
                });

            migrationBuilder.InsertData(
                table: "MyRoles",
                columns: new[] { "MyRoleID", "CreateDate", "IsDelete", "ModifiedDate", "RoleName", "RoleTitle" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9246), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9258), "user", "کاربر" },
                    { 2, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9260), false, new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9260), "Admin", "مدیر فروشگاه" }
                });

            migrationBuilder.InsertData(
                table: "MyUsers",
                columns: new[] { "MYUserID", "Address", "Avatar", "CreateDate", "ForMe", "IsDelete", "Mobile", "ModifiedDate", "MyRoleID", "UserName" },
                values: new object[] { 1, null, "download.png", new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9384), null, false, "9199149932", new DateTime(2024, 4, 6, 17, 51, 8, 775, DateTimeKind.Local).AddTicks(9384), 2, "ادمین" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "MyCategoryID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "MyCategoryID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "MyCategoryID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "MyCategoryID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "MYlevelID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Levels",
                keyColumn: "MYlevelID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MyRoles",
                keyColumn: "MyRoleID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MyUsers",
                keyColumn: "MYUserID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MyRoles",
                keyColumn: "MyRoleID",
                keyValue: 2);
        }
    }
}
