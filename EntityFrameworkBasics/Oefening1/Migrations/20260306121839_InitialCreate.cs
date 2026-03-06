using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Oefening1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, "Classic leather sneakers.", "Retro High-Tops", 120f },
                    { 2, 0, "Breathable mesh with arch support.", "Performance Runners", 89.99f },
                    { 3, 0, "Suede slip-ons for business casual.", "Casual Loafers", 75f },
                    { 4, 0, "Waterproof and rugged terrain ready.", "Hiking Boots", 145.5f },
                    { 5, 1, "Soft-wash cotton graphic tee.", "Vintage Band Tee", 25f },
                    { 6, 1, "Plain white essential tee.", "Basic V-Neck", 15f },
                    { 7, 1, "Limited edition artist print.", "Graphic Streetwear", 45f },
                    { 8, 1, "Lightweight layering piece.", "Long Sleeve Jersey", 30f },
                    { 9, 2, "Stretch khaki for all-day comfort.", "Slim Fit Chinos", 55f },
                    { 10, 2, "Raw indigo denim, straight leg.", "Selvedge Denim", 110f },
                    { 11, 2, "Multi-pocket durable canvas.", "Cargo Work Pants", 65f },
                    { 12, 2, "Tailored wool blend.", "Dress Slacks", 95f },
                    { 13, 3, "Quick-dry fabric for gym sessions.", "Athletic Mesh Shorts", 22.5f },
                    { 14, 3, "Classic 7-inch inseam.", "Flat-Front Chino Shorts", 35f },
                    { 15, 3, "Rugged outdoor shorts.", "Cargo Shorts", 38f },
                    { 16, 3, "", "Board Shorts", 40f },
                    { 17, 4, "Fleece-lined charcoal hoodie.", "Heavyweight Hoodie", 60f },
                    { 18, 4, "Pique knit sporty layer.", "Quarter-Zip Pullover", 52f },
                    { 19, 4, "Minimalist embroidered logo.", "Crewneck Sweater", 48f },
                    { 20, 4, "Water-resistant tech fleece.", "Zip-up Windbreaker", 75f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
