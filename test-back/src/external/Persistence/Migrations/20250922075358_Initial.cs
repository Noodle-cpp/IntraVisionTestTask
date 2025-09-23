using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Banknote = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sodas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImgPath = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sodas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sodas_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SodaId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    Count = table.Column<int>(type: "integer", nullable: false),
                    BrandId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SodaName = table.Column<string>(type: "text", nullable: false),
                    BrandName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carts_Sodas_SodaId",
                        column: x => x.SodaId,
                        principalTable: "Sodas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            InsertTestData(migrationBuilder);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_BrandId",
                table: "Carts",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_SodaId",
                table: "Carts",
                column: "SodaId");

            migrationBuilder.CreateIndex(
                name: "IX_Sodas_BrandId",
                table: "Sodas",
                column: "BrandId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "Sodas");

            migrationBuilder.DropTable(
                name: "Brands");
        }

        private static void InsertTestData(MigrationBuilder migrationBuilder)
        {
            // Генерация GUID для Brands
            var brandId1 = Guid.NewGuid();
            var brandId2 = Guid.NewGuid();
            var brandId3 = Guid.NewGuid();
            var brandId4 = Guid.NewGuid();
            var brandId5 = Guid.NewGuid();
            
            // Генерация GUID для Sodas
            var sodaId1 = Guid.NewGuid();
            var sodaId2 = Guid.NewGuid();
            var sodaId3 = Guid.NewGuid();
            var sodaId4 = Guid.NewGuid();
            var sodaId5 = Guid.NewGuid();
            
            // Генерация GUID для Carts
            var cartId1 = Guid.NewGuid();
            var cartId2 = Guid.NewGuid();
            var cartId3 = Guid.NewGuid();
            var cartId4 = Guid.NewGuid();
            var cartId5 = Guid.NewGuid();
            
            // Заполнение таблицы Brands
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { brandId1, "Coca-Cola" },
                    { brandId2, "Pepsi" },
                    { brandId3, "Fanta" },
                    { brandId4, "Sprite" },
                    { brandId5, "Dr Pepper" }
                });
            
            // Заполнение таблицы Sodas
            migrationBuilder.InsertData(
                table: "Sodas",
                columns: new[] { "Id", "Name", "ImgPath", "Price", "Count", "BrandId" },
                values: new object[,]
                {
                    { 
                        sodaId1, 
                        "Coca-Cola Classic", 
                        "https://media.istockphoto.com/id/458464735/ru/%D1%84%D0%BE%D1%82%D0%BE/coca-cola.jpg?s=612x612&w=0&k=20&c=bbuhFYt5nP5gJTD33i9GjDOudoT-jLlQSSQsO6HwBEA=", 
                        80, 
                        15, 
                        brandId1 
                    },
                    { 
                        sodaId2, 
                        "Pepsi Cola", 
                        "https://thumbs.dreamstime.com/b/%D0%B1%D1%83%D1%82%D1%8B-%D0%BA%D0%B0-%D0%BF%D0%B8%D1%82%D1%8C%D1%8F-%D0%BF%D0%B5%D0%BF%D1%81%D0%B8-%D0%BA%D0%BE-%D0%B0-%D0%BD%D0%B0-%D0%B1%D0%B5-%D0%B8%D0%B7%D0%BD%D0%B5-%D0%B8%D0%B7%D0%BE-%D0%B8%D1%80%D0%BE%D0%B2%D0%B0-%D0%B0-%D0%BF%D1%80%D0%B5-%D0%BF%D0%BE%D1%81%D1%8B-%D0%BA%D1%83-%D0%BC%D0%BE%D0%B6%D0%B5%D1%82-92343928.jpg", 
                        75, 
                        20, 
                        brandId2 
                    },
                    { 
                        sodaId3, 
                        "Fanta Orange", 
                        "https://media.istockphoto.com/id/509533066/ru/%D1%84%D0%BE%D1%82%D0%BE/%D1%8F%D1%80%D0%BA%D0%B8%D0%B9-%D0%BE%D1%80%D0%B0%D0%BD%D0%B6%D0%B5%D0%B2%D1%8B%D0%B9-%D0%BC%D0%BE%D0%B6%D0%B5%D1%82.jpg?s=612x612&w=0&k=20&c=DYfjbduQII50Dq4_WN7FGJIlNDNkeDR-GF3uB4QYfRg=", 
                        70, 
                        18, 
                        brandId3 
                    },
                    { 
                        sodaId4, 
                        "Sprite Lemon", 
                        "https://media.istockphoto.com/id/486786315/ru/%D1%84%D0%BE%D1%82%D0%BE/%D1%81%D0%BF%D1%80%D0%B0%D0%B9%D1%82.jpg?s=612x612&w=0&k=20&c=l94TjVbOJhaWyw0couq8KnSYw9O-Syg2x62vWyKdPdg=", 
                        70, 
                        12, 
                        brandId4 
                    },
                    { 
                        sodaId5, 
                        "Dr Pepper", 
                        "https://media.istockphoto.com/id/458250811/ru/%D1%84%D0%BE%D1%82%D0%BE/%D0%BA%D1%80%D0%B0%D1%81%D0%BD%D1%8B%D0%B9-%D0%B0%D0%BB%D1%8E%D0%BC%D0%B8%D0%BD%D0%B8%D0%B5%D0%B2%D0%B0%D1%8F-%D0%B1%D0%B0%D0%BD%D0%BA%D0%B0-%D0%B4-%D1%80-%D0%BF%D0%B5%D1%80%D0%B5%D1%86.jpg?s=612x612&w=0&k=20&c=uOS3TL_9_7Xy3Nv_g_htfQBqYzuKZmjmxAlujpphhuY=", 
                        85, 
                        10, 
                        brandId5 
                    }
                });
            
            // Заполнение таблицы Carts
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "SodaId", "Price", "Count", "BrandId", "CreatedAt", "SodaName", "BrandName" },
                values: new object[,]
                {
                    { 
                        cartId1, 
                        sodaId1, 
                        80, 
                        2, 
                        brandId1, 
                        DateTime.UtcNow.AddDays(-1), 
                        "Coca-Cola Classic", 
                        "Coca-Cola" 
                    },
                    { 
                        cartId3, 
                        sodaId3, 
                        70, 
                        3, 
                        brandId3, 
                        DateTime.UtcNow.AddHours(-1), 
                        "Fanta Orange", 
                        "Fanta" 
                    },
                    { 
                        cartId4, 
                        sodaId4, 
                        70, 
                        1, 
                        brandId4, 
                        DateTime.UtcNow.AddMinutes(-30), 
                        "Sprite Lemon", 
                        "Sprite" 
                    }
                });
        }
    }
}
