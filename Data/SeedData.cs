using Microsoft.EntityFrameworkCore;
using SirumalaiNaturals.Api.Models;

namespace SirumalaiNaturals.Api.Data;

public static class SeedData
{
    public static async Task InitialiseAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await db.Database.EnsureCreatedAsync();
        if (await db.Categories.AnyAsync()) return;

        var honey = new Category { Name = "Honey", Description = "Natural honey and forest honey", ImageUrl = "https://images.unsplash.com/photo-1587049352851-8d4e89133924?w=500" };
        var spices = new Category { Name = "Spices", Description = "Aromatic hill-grown spices", ImageUrl = "https://images.unsplash.com/photo-1599909533730-f9d4d1b3d4e5?w=500" };
        var fruits = new Category { Name = "Fruits", Description = "Seasonal fruits from the hills", ImageUrl = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=500" };
        var natural = new Category { Name = "Natural Products", Description = "Selected products from local producers", ImageUrl = "https://images.unsplash.com/photo-1470252649378-9c29740c9fa8?w=500" };

        db.Categories.AddRange(honey, spices, fruits, natural);
        await db.SaveChangesAsync();

        var products = new List<Product>
        {
            new()
            {
                Name = "Sirumalai Natural Honey", Description = "A naturally sweet honey product sourced from local hill producers. Product details should be verified before sale.",
                Origin = "Sirumalai Hills, Tamil Nadu", ImageUrl = "https://images.unsplash.com/photo-1587049352851-8d4e89133924?w=900",
                Price = 450m, Unit = "500 g", IsFeatured = true, CategoryId = honey.Id,
                Variants = [new() { Name = "250 g", Price = 240m }, new() { Name = "500 g", Price = 450m }, new() { Name = "1 kg", Price = 850m }]
            },
            new()
            {
                Name = "Sirumalai Black Pepper", Description = "Whole black pepper for everyday cooking. Confirm grade and origin with your supplier.",
                Origin = "Sirumalai Hills, Tamil Nadu", ImageUrl = "/images/black-pepper.jpg",
                Price = 280m, Unit = "250 g", IsFeatured = true, CategoryId = spices.Id,
                Variants = [new() { Name = "100 g", Price = 120m }, new() { Name = "250 g", Price = 280m }, new() { Name = "500 g", Price = 540m }]
            },
            new()
            {
                Name = "Sirumalai Hill Banana", Description = "Seasonal hill bananas. Actual availability and price may vary by harvest.",
                Origin = "Sirumalai Hills, Tamil Nadu", ImageUrl = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=900",
                Price = 120m, Unit = "1 kg", IsFeatured = true, CategoryId = fruits.Id,
                Variants = [new() { Name = "1 kg", Price = 120m }, new() { Name = "2 kg", Price = 230m }]
            },
            new()
            {
                Name = "Hill Herb Selection", Description = "A sample listing for a locally sourced natural product. Replace with a product you actually stock.",
                Origin = "Sirumalai region", ImageUrl = "https://images.unsplash.com/photo-1518531933037-91b2f5f229cc?w=900",
                Price = 180m, Unit = "Pack", IsFeatured = false, CategoryId = natural.Id,
                Variants = [new() { Name = "1 pack", Price = 180m }]
            }
        };

        db.Products.AddRange(products);
        await db.SaveChangesAsync();
    }
}
