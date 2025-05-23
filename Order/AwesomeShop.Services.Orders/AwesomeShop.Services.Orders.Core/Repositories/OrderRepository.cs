using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Core.Entitties;

namespace AwesomeShop.Services.Orders.Core.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public Task AddAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}