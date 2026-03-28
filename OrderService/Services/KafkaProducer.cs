using Confluent.Kafka;
using System.Text.Json;

namespace OrderService.Services
{
    public class KafkaProducer
    {
        private readonly ProducerConfig _config;

        public KafkaProducer()
        {
            _config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };
        }

        public async Task PublishAsync(string topic, object data)
        {
            using var producer = new ProducerBuilder<Null, string>(_config).Build();

            var message = new Message<Null, string>
            {
                Value = JsonSerializer.Serialize(data)
            };

            await producer.ProduceAsync(topic, message);
        }
    }
}
