using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Cats.Migrations
{
    /// <inheritdoc />
    public partial class deletehumantable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Human_HumanId",
                table: "Cats");

            migrationBuilder.DropTable(
                name: "Human");

            migrationBuilder.DropIndex(
                name: "IX_Cats_HumanId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "HumanId",
                table: "Cats");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HumanId",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Human",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Human", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_HumanId",
                table: "Cats",
                column: "HumanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Human_HumanId",
                table: "Cats",
                column: "HumanId",
                principalTable: "Human",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
