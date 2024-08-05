using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    menu_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    amblem = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Menu__4CA0FADC3E077BAA", x => x.menu_id);
                    table.ForeignKey(
                        name: "FK__Menu__parent_id__398D8EEE",
                        column: x => x.parent_id,
                        principalTable: "Menu",
                        principalColumn: "menu_id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370FDD334116", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    car_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    photoPath = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cars__4C9A0DB3759964D0", x => x.car_id);
                    table.ForeignKey(
                        name: "FK__Cars__menu_id__3D5E1FD2",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "menu_id");
                    table.ForeignKey(
                        name: "FK__Cars__user_id__3C69FB99",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homes",
                columns: table => new
                {
                    home_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    photoPath = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Homes__8ED7E2137B8363FF", x => x.home_id);
                    table.ForeignKey(
                        name: "FK__Homes__menu_id__412EB0B6",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "menu_id");
                    table.ForeignKey(
                        name: "FK__Homes__user_id__403A8C7D",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orders__4659622964ACD4C3", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__Orders__menu_id__70DDC3D8",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "menu_id");
                    table.ForeignKey(
                        name: "FK__Orders__user_id__6FE99F9F",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_menu_id",
                table: "Cars",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_user_id",
                table: "Cars",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_menu_id",
                table: "Homes",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Homes_user_id",
                table: "Homes",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_parent_id",
                table: "Menu",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_menu_id",
                table: "Orders",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Homes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
