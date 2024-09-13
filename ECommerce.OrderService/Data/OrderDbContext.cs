using ECommerce.Model;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.OrderService.Data;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<OrderModel> Orders { get; set; }
}