using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Services;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : Controller
    {
        private readonly KafkaProducer _producer;

        public OrdersController()
        {
            _producer = new KafkaProducer();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            await _producer.PublishAsync("order-created", order);

            return Ok(new
            {
                Message = "Order Created and evnent published",
                Data = order


            });
        }


    }
}
