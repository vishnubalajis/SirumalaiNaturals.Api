namespace SirumalaiNaturals.Api.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Origin { get; set; } = "Sirumalai Hills";
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Unit { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public bool IsFeatured { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public List<ProductVariant> Variants { get; set; } = [];
}
