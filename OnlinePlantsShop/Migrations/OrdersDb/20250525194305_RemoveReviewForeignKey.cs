using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlinePlantsShop.Migrations.OrdersDb
{
    /// <inheritdoc />
    public partial class RemoveReviewForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Plant_PlantId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Plant");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PlantId",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plant", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PlantId",
                table: "Reviews",
                column: "PlantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Plant_PlantId",
                table: "Reviews",
                column: "PlantId",
                principalTable: "Plant",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
