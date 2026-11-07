using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirumalaiNaturals.Api.Data;
using SirumalaiNaturals.Api.Dtos;
using SirumalaiNaturals.Api.Models;

namespace SirumalaiNaturals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAll(
        [FromQuery] int? categoryId,
        [FromQuery] string? search,
        [FromQuery] bool? featured,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 12)
    {
        page = Math.Max(page, 1);
        pageSize = Math.Clamp(pageSize, 1, 50);

        IQueryable<Product> query = db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .Where(p => p.IsAvailable);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            query = query.Where(p => p.Name.Contains(term) || p.Description.Contains(term));
        }

        if (featured.HasValue)
            query = query.Where(p => p.IsFeatured == featured.Value);

        var entities = await query
            .OrderByDescending(p => p.IsFeatured)
            .ThenBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        // Map to DTOs after materializing because ToDto is application code,
        // not a SQL expression that Entity Framework can translate.
        var products = entities.Select(ToDto).ToList();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetById(int id)
    {
        var product = await db.Products
            .AsNoTracking()
            .Include(p => p.Category)
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsAvailable);

        return product is null ? NotFound() : Ok(ToDto(product));
    }

    private static ProductDto ToDto(Product p) => new(
        p.Id, p.Name, p.Description, p.Origin, p.ImageUrl, p.Price, p.Unit,
        p.IsAvailable, p.IsFeatured, p.CategoryId, p.Category?.Name ?? "",
        p.Variants.OrderBy(v => v.Price)
            .Select(v => new ProductVariantDto(v.Id, v.Name, v.Price, v.IsAvailable))
            .ToList());
}
