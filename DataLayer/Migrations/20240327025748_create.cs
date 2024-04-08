using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    MyCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Isdelete = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.MyCategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                columns: table => new
                {
                    MYlevelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Levels", x => x.MYlevelID);
                });

            migrationBuilder.CreateTable(
                name: "MyRoles",
                columns: table => new
                {
                    MyRoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    RoleTitle = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyRoles", x => x.MyRoleID);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    IdTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<int>(type: "int", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.IdTransaction);
                });

            migrationBuilder.CreateTable(
                name: "MyProducts",
                columns: table => new
                {
                    MyProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MyCategoryID = table.Column<int>(type: "int", nullable: false),
                    MYlevelID = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DurationData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Createdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Isdelete = table.Column<bool>(type: "bit", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyProducts", x => x.MyProductId);
                    table.ForeignKey(
                        name: "FK_MyProducts_Categories_MyCategoryID",
                        column: x => x.MyCategoryID,
                        principalTable: "Categories",
                        principalColumn: "MyCategoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyProducts_Levels_MYlevelID",
                        column: x => x.MYlevelID,
                        principalTable: "Levels",
                        principalColumn: "MYlevelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MyUsers",
                columns: table => new
                {
                    MYUserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ForMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MyRoleID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyUsers", x => x.MYUserID);
                    table.ForeignKey(
                        name: "FK_MyUsers_MyRoles_MyRoleID",
                        column: x => x.MyRoleID,
                        principalTable: "MyRoles",
                        principalColumn: "MyRoleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MYUserID = table.Column<int>(type: "int", nullable: false),
                    MyProductId = table.Column<int>(type: "int", nullable: false),
                    IdTransaction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_MyProducts_MyProductId",
                        column: x => x.MyProductId,
                        principalTable: "MyProducts",
                        principalColumn: "MyProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_MyUsers_MYUserID",
                        column: x => x.MYUserID,
                        principalTable: "MyUsers",
                        principalColumn: "MYUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Transactions_IdTransaction",
                        column: x => x.IdTransaction,
                        principalTable: "Transactions",
                        principalColumn: "IdTransaction",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_IdTransaction",
                table: "Items",
                column: "IdTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MyProductId",
                table: "Items",
                column: "MyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MYUserID",
                table: "Items",
                column: "MYUserID");

            migrationBuilder.CreateIndex(
                name: "IX_MyProducts_MyCategoryID",
                table: "MyProducts",
                column: "MyCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MyProducts_MYlevelID",
                table: "MyProducts",
                column: "MYlevelID");

            migrationBuilder.CreateIndex(
                name: "IX_MyUsers_MyRoleID",
                table: "MyUsers",
                column: "MyRoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MyProducts");

            migrationBuilder.DropTable(
                name: "MyUsers");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Levels");

            migrationBuilder.DropTable(
                name: "MyRoles");
        }
    }
}
