using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus
{
    public class RabbitMqClient : IMessageBusClient
    {
        private readonly IConnection _connection;

        public RabbitMqClient(ProducerConnection producerConnection)
        {
            _connection = producerConnection.Connection;
        }
        public void Publish(object message, string routtingKey, string exchange)
        {
            var channel = _connection.CreateModel();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var playload = JsonConvert.SerializeObject(message);

            var body = Encoding.UTF8.GetBytes(playload);

            channel.ExchangeDeclare(exchange, "topic", true);

            channel.BasicPublish(exchange, routtingKey, null, body);
        }
    }
}