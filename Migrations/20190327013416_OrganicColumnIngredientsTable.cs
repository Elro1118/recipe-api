using Microsoft.EntityFrameworkCore.Migrations;

namespace recipe_api.Migrations
{
    public partial class OrganicColumnIngredientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Organic",
                table: "Ingredients",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Organic",
                table: "Ingredients");
        }
    }
}
