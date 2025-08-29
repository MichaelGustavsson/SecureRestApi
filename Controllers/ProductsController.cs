using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productsApi.Data;

namespace productsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<ActionResult> ListAllProducts()
        {
            var products = await _context.Products.ToListAsync();

            return Ok(new { Success = true, data = products });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(string id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return NotFound();

            return Ok(new { Success = true, data = product });
        }
    }
}
