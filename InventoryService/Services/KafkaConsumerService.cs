using Confluent.Kafka;

namespace InventoryService.Services
{
    public class KafkaConsumerService:BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "localhost:9092",
                    GroupId = "inventory-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

                consumer.Subscribe("order-created");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);

                        Console.WriteLine("📦 Inventory Service Received:");
                        Console.WriteLine(result.Message.Value);

                        // 👉 Here you will later update DB / stock
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
            }, stoppingToken);
        }
    }
}
