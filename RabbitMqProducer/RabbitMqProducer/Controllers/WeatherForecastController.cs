using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMqProducer.Models;

namespace RabbitMqProducer.Controllers
{
    [ApiController]
    [Route("api")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("start")]
        public IActionResult Start()
        {
            return Ok("Started Producer");
        }
        [HttpPost("message")]
        public IActionResult SendMessage([FromBody] Message request)
        {
            var factory = new ConnectionFactory();
            factory.UserName = "orxansenior";
            factory.Password = "hamidaz12";
            factory.VirtualHost = "/";
            factory.HostName = "192.168.0.104";
            factory.Port = AmqpTcpEndpoint.UseDefaultPort;

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-message",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var message = request;
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
            channel.BasicPublish("", "demo-message", null, body);
            return Ok(new { SendedMessage = request });
        }
    }
}
