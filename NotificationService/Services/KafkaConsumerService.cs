using Confluent.Kafka;

namespace NotificationService.Services
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
                    GroupId = "notification-group", // IMPORTANT: different group
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

                consumer.Subscribe("order-created");

                Console.WriteLine("📧 Notification Service Started...");

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = consumer.Consume(stoppingToken);

                        Console.WriteLine("=================================");
                        Console.WriteLine("📧 Notification Service Received:");
                        Console.WriteLine(result.Message.Value);
                        Console.WriteLine("📬 Sending notification (email/sms simulation)");
                        Console.WriteLine("=================================");
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
