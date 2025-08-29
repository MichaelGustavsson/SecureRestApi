using Microsoft.EntityFrameworkCore;
using productsApi.Models;

namespace productsApi.Data;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
}
