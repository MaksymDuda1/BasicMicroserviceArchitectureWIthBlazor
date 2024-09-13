using Confluent.Kafka;
using ECommerce.Model;
using ECommerce.OrderService.Data;
using ECommerce.OrderService.Kafka;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ECommerce.OrderService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(OrderDbContext context, IKafkaProducer producer) : ControllerBase
{
    [HttpGet]
    public async Task<List<OrderModel>> GetOrders()
    {
        return await context.Orders.ToListAsync();
    }

    [HttpPost]
    public async Task<OrderModel> PostOrder(OrderModel order)
    {
        order.OrderDate = DateTime.Now;
        context.Orders.Add(order);
        await context.SaveChangesAsync();

        await producer.ProduceAsync("order-topic", new Message<string, string>
        {
            Key = order.Id.ToString(),
            Value = JsonConvert.SerializeObject(order)
        });
        
        return order;
    }
}