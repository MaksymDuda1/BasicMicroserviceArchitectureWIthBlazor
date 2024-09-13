using ECommerce.Model;
using ECommerce.ProductService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.ProductService;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ProductDbContext context): ControllerBase
{
    [HttpGet]
    public async Task<List<ProductModel>> GetProducts()
    {
        Console.WriteLine("GetProducts");
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ProductModel> GetProduct(int id)
    {
        return await context.Products.FindAsync(id);
    }
}