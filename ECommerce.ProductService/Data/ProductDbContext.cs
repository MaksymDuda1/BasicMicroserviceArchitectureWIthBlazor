using ECommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductService.Data;

public sealed class ProductDbContext(DbContextOptions<ProductDbContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 1, Name = "Shirt", Price = 10, Quantity = 20});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 2, Name = "Pant", Price = 50, Quantity = 50});
        modelBuilder.Entity<ProductModel>().HasData(new ProductModel {Id = 3, Name = "Polo", Price = 20, Quantity = 20});
    }
    
    public DbSet<ProductModel> Products { get; set; }
}