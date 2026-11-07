namespace SirumalaiNaturals.Api.Models;

public class ProductVariant
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public string Name { get; set; } = string.Empty; // e.g. 250 g, 500 g, 1 kg
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; } = true;
}
