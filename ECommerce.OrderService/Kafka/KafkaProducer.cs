using Confluent.Kafka;

namespace ECommerce.OrderService.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, Message<string, string> message);
}

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> producer;

    public KafkaProducer()
    {
        var config = new ConsumerConfig
        {
            GroupId = "order-group",
            BootstrapServers = "localhost:9092",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        
        producer = new ProducerBuilder<string, string>(config).Build();
    }
    
    public Task ProduceAsync(string topic, Message<string, string> message)
    {
        return producer.ProduceAsync(topic, message);
    }
}