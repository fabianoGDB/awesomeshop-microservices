using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Core.Entitties;

namespace AwesomeShop.Services.Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderById(Guid id);
        Task AddAsync(Order order);
        Task UpdateAsync(Order order);
    }
}