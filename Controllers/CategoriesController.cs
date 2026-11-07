using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SirumalaiNaturals.Api.Data;
using SirumalaiNaturals.Api.Dtos;

namespace SirumalaiNaturals.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetAll()
    {
        var categories = await db.Categories
            .AsNoTracking()
            .Where(c => c.IsActive)
            .OrderBy(c => c.Name)
            .Select(c => new CategoryDto(c.Id, c.Name, c.Description, c.ImageUrl))
            .ToListAsync();

        return Ok(categories);
    }
}
