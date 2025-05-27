using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Application.Extensions;
using AwesomeShop.Services.Orders.Core.Entitties;
using AwesomeShop.Services.Orders.Core.Repositories;
using AwesomeShop.Services.Orders.Infrastructure.MessageBus;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Commands.Handlers
{
    public class AddOrderHandler : IRequestHandler<AddOrder, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMessageBusClient _messageBus;

        public AddOrderHandler(IOrderRepository orderRepository, IMessageBusClient messageBus)
        {
            _orderRepository = orderRepository;
            _messageBus = messageBus;
        }

        public async Task<Guid> Handle(AddOrder request, CancellationToken cancellationToken)
        {
            var order = request.ToEntity();

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