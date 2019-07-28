using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ITDB.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentCategoryID = table.Column<int>(nullable: true),
                    Logo = table.Column<string>(maxLength: 255, nullable: false),
                    BannerLogo = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Sort = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigureCategories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    IfShow = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Data = table.Column<string>(maxLength: 255, nullable: true),
                    Url = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigureCategories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurePositions",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Position = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurePositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ConfigureSliders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    IfShow = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    PositionID = table.Column<int>(nullable: false),
                    Url = table.Column<string>(maxLength: 255, nullable: true),
                    ImgUrl = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigureSliders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DBOrderDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OrderDetailID = table.Column<Guid>(nullable: false),
                    DBPeriodsID = table.Column<int>(nullable: false),
                    DBTicket = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBOrderDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DBPeriods",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsID = table.Column<int>(nullable: false),
                    PeriodsCode = table.Column<int>(nullable: false),
                    NeedNum = table.Column<int>(nullable: false),
                    GoodsPrice = table.Column<decimal>(nullable: false),
                    PerPrice = table.Column<decimal>(nullable: false),
                    OverplusNum = table.Column<int>(nullable: false),
                    IfOpen = table.Column<bool>(nullable: false),
                    OpenTime = table.Column<DateTime>(nullable: true),
                    WaitOpenTime = table.Column<DateTime>(nullable: true),
                    LuckyUserID = table.Column<int>(nullable: true),
                    LuckyCode = table.Column<int>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBPeriods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GoodsName = table.Column<string>(maxLength: 50, nullable: true),
                    GoodsDescribe = table.Column<string>(maxLength: 255, nullable: true),
                    GoodsLogo2 = table.Column<string>(maxLength: 255, nullable: true),
                    GoodsLogo = table.Column<string>(maxLength: 255, nullable: true),
                    GoodPrice = table.Column<decimal>(nullable: false),
                    IfShow = table.Column<bool>(nullable: false),
                    GoodsDetail = table.Column<string>(nullable: true),
                    CategoryID = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OrderID = table.Column<Guid>(nullable: false),
                    DBPeriodsID = table.Column<int>(nullable: false),
                    DBNum = table.Column<int>(nullable: false),
                    DBPrice = table.Column<decimal>(nullable: false),
                    DBPerPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    OrderCode = table.Column<string>(maxLength: 36, nullable: true),
                    IfPay = table.Column<bool>(nullable: false),
                    SubmitTime = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    PayTime = table.Column<DateTime>(nullable: true),
                    PostUserName = table.Column<string>(maxLength: 20, nullable: true),
                    PostUserPhone = table.Column<string>(maxLength: 11, nullable: true),
                    PostAddress = table.Column<string>(maxLength: 255, nullable: true),
                    PostDetailAddress = table.Column<string>(maxLength: 255, nullable: true),
                    OrderStatus = table.Column<int>(nullable: false),
                    IpAddress = table.Column<string>(maxLength: 20, nullable: true),
                    IpCity = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RechargeRecords",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ChargeCode = table.Column<string>(maxLength: 36, nullable: true),
                    ChargeMoney = table.Column<decimal>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RechargeRecords", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShippingAddress",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IfDefault = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    UserPhone = table.Column<string>(maxLength: 50, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    DetailAddress = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddress", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShopCarts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DBPeriodsID = table.Column<int>(nullable: false),
                    Num = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    GoodsID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCarts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShowOrders",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    DBPeriodsID = table.Column<int>(nullable: false),
                    img1 = table.Column<string>(maxLength: 255, nullable: true),
                    img6 = table.Column<string>(maxLength: 255, nullable: true),
                    img5 = table.Column<string>(maxLength: 255, nullable: true),
                    img4 = table.Column<string>(maxLength: 255, nullable: true),
                    img3 = table.Column<string>(maxLength: 255, nullable: true),
                    img2 = table.Column<string>(maxLength: 255, nullable: true),
                    Title = table.Column<string>(maxLength: 255, nullable: true),
                    content = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShowOrders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    IsComplete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOtherLogins",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    Key1 = table.Column<string>(maxLength: 36, nullable: true),
                    Key2 = table.Column<string>(maxLength: 36, nullable: true),
                    icon = table.Column<string>(maxLength: 255, nullable: true),
                    nickname = table.Column<string>(maxLength: 50, nullable: true),
                    Code = table.Column<string>(maxLength: 36, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOtherLogins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserPwd = table.Column<string>(maxLength: 50, nullable: true),
                    UserLogo = table.Column<string>(maxLength: 255, nullable: true),
                    UserPhone = table.Column<string>(maxLength: 11, nullable: true),
                    UserBalance = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ConfigureCategories");

            migrationBuilder.DropTable(
                name: "ConfigurePositions");

            migrationBuilder.DropTable(
                name: "ConfigureSliders");

            migrationBuilder.DropTable(
                name: "DBOrderDetails");

            migrationBuilder.DropTable(
                name: "DBPeriods");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "RechargeRecords");

            migrationBuilder.DropTable(
                name: "ShippingAddress");

            migrationBuilder.DropTable(
                name: "ShopCarts");

            migrationBuilder.DropTable(
                name: "ShowOrders");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "UserOtherLogins");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
