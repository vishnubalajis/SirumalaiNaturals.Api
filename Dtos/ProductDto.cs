namespace SirumalaiNaturals.Api.Dtos;

public record ProductVariantDto(int Id, string Name, decimal Price, bool IsAvailable);

public record ProductDto(
    int Id,
    string Name,
    string Description,
    string Origin,
    string ImageUrl,
    decimal Price,
    string Unit,
    bool IsAvailable,
    bool IsFeatured,
    int CategoryId,
    string CategoryName,
    IReadOnlyList<ProductVariantDto> Variants);
