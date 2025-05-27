using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Application.Dtos;
using AwesomeShop.Services.Orders.Application.Extensions;
using AwesomeShop.Services.Orders.Core.Entitties;
using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;
using AwesomeShop.Services.Orders.Infrastructure.ServiceDiscovery;
using AwesomeShop.Services.Orders.Application;
using MediatR;
using Newtonsoft.Json;

namespace AwesomeShop.Services.Orders.Application.Commands.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBusClient _messageBus;
        private readonly IServiceDiscoveryService _serviceDiscoveryService;

        public AddOrderHandler(IOrderRepository orderRepository, IMessageBusClient messageBus, IServiceDiscoveryService serviceDiscoveryService)
        {
            _orderRepository = orderRepository;
            _messageBus = messageBus;
            _serviceDiscoveryService = serviceDiscoveryService;
        }

        public async Task<Guid> Handle(AddOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToEntity();

            var customerUrl = await _serviceDiscoveryService.GetServiceUri("CustomerServices", $"/api/customers/{order.Customer.Id}");

            var httpClient = new HttpClient();

            var result = await httpClient.GetAsync(customerUrl);
            var stringResult = await result.Content.ReadAsStringAsync();


            var customerDto = JsonConvert.DeserializeObject(stringResult);

            Console.WriteLine(stringResult);

            await _orderRepository.AddAsync(order);

            foreach (var evnt in order.Events)
            {
                var routtingKey = evnt.GetType().Name.ToDashCase();
                _messageBus.Publish(evnt, routtingKey, "order-service");
            }

            return order.Id;
        }
    }
}