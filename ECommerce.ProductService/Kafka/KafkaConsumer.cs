using Confluent.Kafka;
using ECommerce.Model;
using ECommerce.ProductService.Data;
using Newtonsoft.Json;

namespace ECommerce.ProductService.Kafka;

public class KafkaConsumer(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(() =>
        {
            _ = ConsumeAsync("order-topic", stoppingToken);
        }, stoppingToken);
    }

    public async Task ConsumeAsync(string topic, CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            GroupId = "order-topic",
            BootstrapServers = $"localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(topic);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);
            
            var order = JsonConvert.DeserializeObject<OrderModel>(consumeResult.Message.Value);
            using var scope = scopeFactory.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            
            var product = await db.Products.FindAsync(order.ProductId);
            if (product != null)
            {
                product.Quantity -= order.Quantity;
                await db.SaveChangesAsync();
            }
        }
        
        consumer.Close();
    }
}