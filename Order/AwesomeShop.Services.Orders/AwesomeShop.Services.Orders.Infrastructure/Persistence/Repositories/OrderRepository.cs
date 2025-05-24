using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Core.Entitties;
using AwesomeShop.Services.Orders.Core.Repositories;
using MongoDB.Driver;

namespace AwesomeShop.Services.Orders.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _colletion;

        public OrderRepository(IMongoDatabase mongoDatabase)
        {
            _colletion = mongoDatabase.GetCollection<Order>("orders");
        }
        public async Task AddAsync(Order order)
        {
            await _colletion.InsertOneAsync(order);
        }

        public async Task<Order> GetOrderById(Guid id)
        {
            return await _colletion.Find(c => c.Id == id).SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            await _colletion.ReplaceOneAsync(c => c.Id == order.Id, order);
        }
    }
}