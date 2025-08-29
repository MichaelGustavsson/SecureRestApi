using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productsApi.Data;
using productsApi.Shared;
using productsApi.Utilities;
using productsApi.Utilities.Attributes;

namespace productsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(AppDbContext context, IApiKeyValidator apiKeyValidator) : ControllerBase
    {
        private readonly AppDbContext _context = context;
        private readonly IApiKeyValidator _validator = apiKeyValidator;

        [HttpGet]
        public async Task<ActionResult> ListAllProducts()
        {
            var apiKey = Request.Headers[Constants.HeaderName];

            if (string.IsNullOrWhiteSpace(apiKey)) return BadRequest();
            if (!_validator.IsValidKey(apiKey!)) return Unauthorized();

            var products = await _context.Products.ToListAsync();

            return Ok(new { Success = true, data = products });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProduct(string id)
        {
            var apiKey = Request.Headers[Constants.HeaderName];

            if (string.IsNullOrWhiteSpace(apiKey)) return BadRequest();
            if (!_validator.IsValidKey(apiKey!)) return Unauthorized();

            var product = await _context.Products.FindAsync(id);

            if (product is null) return NotFound();

            return Ok(new { Success = true, data = product });
        }
    }
}
