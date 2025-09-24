using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountToCoin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Coins",
                type: "integer",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "Banknote", "Count" },
                values: new object[,]
                {
                    { 
                        Guid.NewGuid(),
                        1,
                        100
                    },
                    { 
                        Guid.NewGuid(),
                        2,
                        80
                    },
                    { 
                        Guid.NewGuid(),
                        5,
                        60
                    },
                    { 
                        Guid.NewGuid(),
                        10,
                        40
                    },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Coins");
        }
    }
}
