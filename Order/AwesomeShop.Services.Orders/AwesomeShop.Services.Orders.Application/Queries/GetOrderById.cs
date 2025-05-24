using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Application.Dtos.ViewModels;
using MediatR;

namespace AwesomeShop.Services.Orders.Application.Queries
{
    public class GetOrderById : IRequest<OrderViewModel>
    {
        public Guid Id { get; private set; }

        public GetOrderById(Guid id)
        {
            Id = id;
        }
    }
}