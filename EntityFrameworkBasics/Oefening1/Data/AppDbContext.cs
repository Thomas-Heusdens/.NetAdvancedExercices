using Microsoft.EntityFrameworkCore;
using Oefening1.Models;

namespace Oefening1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>().HasData(
                // --- SHOES ---
                new ProductModel { Id = 1, Name = "Retro High-Tops", Category = Category.SHOES, Description = "Classic leather sneakers.", Price = 120.00f },
                new ProductModel { Id = 2, Name = "Performance Runners", Category = Category.SHOES, Description = "Breathable mesh with arch support.", Price = 89.99f },
                new ProductModel { Id = 3, Name = "Casual Loafers", Category = Category.SHOES, Description = "Suede slip-ons for business casual.", Price = 75.00f },
                new ProductModel { Id = 4, Name = "Hiking Boots", Category = Category.SHOES, Description = "Waterproof and rugged terrain ready.", Price = 145.50f },

                // --- T-SHIRTS ---
                new ProductModel { Id = 5, Name = "Vintage Band Tee", Category = Category.T_SHIRTS, Description = "Soft-wash cotton graphic tee.", Price = 25.00f },
                new ProductModel { Id = 6, Name = "Basic V-Neck", Category = Category.T_SHIRTS, Description = "Plain white essential tee.", Price = 15.00f },
                new ProductModel { Id = 7, Name = "Graphic Streetwear", Category = Category.T_SHIRTS, Description = "Limited edition artist print.", Price = 45.00f },
                new ProductModel { Id = 8, Name = "Long Sleeve Jersey", Category = Category.T_SHIRTS, Description = "Lightweight layering piece.", Price = 30.00f },

                // --- PANTS ---
                new ProductModel { Id = 9, Name = "Slim Fit Chinos", Category = Category.PANTS, Description = "Stretch khaki for all-day comfort.", Price = 55.00f },
                new ProductModel { Id = 10, Name = "Selvedge Denim", Category = Category.PANTS, Description = "Raw indigo denim, straight leg.", Price = 110.00f },
                new ProductModel { Id = 11, Name = "Cargo Work Pants", Category = Category.PANTS, Description = "Multi-pocket durable canvas.", Price = 65.00f },
                new ProductModel { Id = 12, Name = "Dress Slacks", Category = Category.PANTS, Description = "Tailored wool blend.", Price = 95.00f },

                // --- SHORTS ---
                new ProductModel { Id = 13, Name = "Athletic Mesh Shorts", Category = Category.SHORTS, Description = "Quick-dry fabric for gym sessions.", Price = 22.50f },
                new ProductModel { Id = 14, Name = "Flat-Front Chino Shorts", Category = Category.SHORTS, Description = "Classic 7-inch inseam.", Price = 35.00f },
                new ProductModel { Id = 15, Name = "Cargo Shorts", Category = Category.SHORTS, Description = "Rugged outdoor shorts.", Price = 38.00f },
                new ProductModel { Id = 16, Name = "Board Shorts", Category = Category.SHORTS, Description = "", Price = 40.00f },

                // --- SWEATSHIRTS ---
                new ProductModel { Id = 17, Name = "Heavyweight Hoodie", Category = Category.SWEATSHIRT, Description = "Fleece-lined charcoal hoodie.", Price = 60.00f },
                new ProductModel { Id = 18, Name = "Quarter-Zip Pullover", Category = Category.SWEATSHIRT, Description = "Pique knit sporty layer.", Price = 52.00f },
                new ProductModel { Id = 19, Name = "Crewneck Sweater", Category = Category.SWEATSHIRT, Description = "Minimalist embroidered logo.", Price = 48.00f },
                new ProductModel { Id = 20, Name = "Zip-up Windbreaker", Category = Category.SWEATSHIRT, Description = "Water-resistant tech fleece.", Price = 75.00f }
            );
        }
    }
}
