using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace AwesomeShop.Services.Orders.Infrastructure.MessageBus
{
    public class ProducerConnection
    {
        public IConnection Connection { get; private set; }

        public ProducerConnection(IConnection connection)
        {
            Connection = connection;
        }
    }
}